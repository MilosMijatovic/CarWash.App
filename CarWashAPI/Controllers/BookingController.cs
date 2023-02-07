using AutoMapper;
using CarWashAPI.DTOs.cs;
using CarWashAPI.Entites;
using CarWashAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWashAPI.Controllers.cs
{

    [Route("api/customusers")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
       

        public BookingController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            
        }

        [HttpGet("/getShops")]
        public async Task<ActionResult<List<ShopDetailsDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            
            var queryable = context.Shops.Include(x => x.ShopsServices).ThenInclude(x => x.Service).AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var shops = await queryable.Paginate(pagination).ToListAsync();
            var shopsDTO = mapper.Map<List<ShopDetailsDTO>>(shops);
            return shopsDTO;


        }


        [HttpGet("/getShopsBy/{id}")]
        public async Task<ActionResult<ShopDetailsDTO>> Get(int id)
        {

            var shop = await context.Shops
                .Include(x => x.ShopsServices).ThenInclude(x => x.Service)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (shop == null)
            {
                return NotFound();
            }
            return mapper.Map<ShopDetailsDTO>(shop);
        }



        [HttpPost("/Booking")] // rezervation
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Consumer")]
        public async Task<ActionResult> PostReservation ([FromBody] BookingCreationDTO bookingCreation)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=> x.UserName == HttpContext.User.Identity.Name);
            var shopsService = await context.ShopServices
                .Include(x => x.Shop)
                .Include(x => x.Service)
                .FirstOrDefaultAsync(x => x.ShopId == bookingCreation.ShopId && x.ServiceId == bookingCreation.ServiceId);
                
            var booking = mapper.Map<Booking>(bookingCreation);
            booking.ConsumerId = user.Id;
            context.Add(booking);
            await context.SaveChangesAsync();

            var bookingDTO = mapper.Map<BookingDTO>(booking);
            bookingDTO.ShopName = shopsService.Shop.ShopName;
            bookingDTO.ServiceName = shopsService.Service.TypeOfService;

            return Ok(bookingDTO);

          
        }
        [HttpDelete("/Cancel/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            var booking = context.Bookings.FirstOrDefault(x => x.Id == id);
            if(booking == null)
            {
                return NotFound();
            }

            if(DateTime.Now.AddMinutes(15) >  booking.Reserved)
            {
                return BadRequest("To late to delete");
            }

            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();

            return NoContent();
        } 

    }
}
