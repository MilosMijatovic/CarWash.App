using AutoMapper;
using CarWashAPI.DTOs.cs;
using CarWashAPI.Entites;
using CarWashAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace CarWashAPI.Controllers.cs
{

    [ApiController]
    [Route("api/shops")]
    public class ShopController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ShopController(ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
            
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult> PostShop([FromForm] ShopCreationDTO shopCreationDTO)
        {
            var shop = mapper.Map<Shop>(shopCreationDTO);
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);

            
            if (user != null)
            {
                shop.OwnerId = user.Id;
            }
            else
            {
                return BadRequest();
            }
            context.Shops.Add(shop);
            await context.SaveChangesAsync();
            var shopDTO = mapper.Map<ShopDTO>(shop);

            return StatusCode(201);
        }
        


        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult> PutShop(int id, [FromBody] ShopCreationDTO shopCreation)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            var shop = mapper.Map<Shop>(shopCreation);
            shop.Id = id;
            
            if (user != null)
            {
                shop.OwnerId = user.Id;
            }
            else
            {
                return BadRequest();
            }

            context.Entry(shop).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("/deleteShop/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult> DeleteShop(int id)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            var exists = await context.Shops.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Shop() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
        

        [HttpGet("/allBooking")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult<List<BookingDTO>>> GetBooking()
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);

            var booking = await context.Bookings
                .Include(x => x.Service)
                .Include(x => x.Shop)
                .AsNoTracking()
                .Where(x => x.Shop.OwnerId == user.Id)
                .ToListAsync();
            var bookingDTO = mapper.Map<List<BookingDTO>>(booking);
            return bookingDTO;
        }

        [HttpDelete("/cancelingBooking/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            var booking = context.Bookings.FirstOrDefault(x => x.Id == id);
            if(booking == null)
            {
                return BadRequest();

            }
            if(DateTime.Now.AddHours(1) > booking.Reserved)
            {
                return BadRequest("Request not alowed");
            }
            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
            return Ok();
        }
   

    }
}
