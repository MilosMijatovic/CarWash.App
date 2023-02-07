
using AutoMapper;
using CarWashAPI.DTOs.cs;
using CarWashAPI.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWashAPI.Controllers.cs
{

    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase

    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
      

        public ServiceController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
           
        }

        [HttpGet("allServices")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult<List<ServiceDTO>>> Get()
        {
            var services = await context.Services.AsNoTracking().ToListAsync();
            var serviceDTO = mapper.Map<List<ServiceDTO>>(services);
            return serviceDTO;
        }

        [HttpGet("api/getServiceby/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult<ServiceDTO>> Get(int id)
        {
            var service = await context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            var serviceDTO = mapper.Map<ServiceDTO>(service);
            return serviceDTO;
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult>Put(int id , [FromBody] ServiceCreationDTO serviceCreation)
        {
            var service = mapper.Map<Service>(serviceCreation);
                service.Id = id;
            context.Entry(service).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Services.AnyAsync(x => x.Id == id);  
            if (!exists)
            {
                return NotFound();
            }
            context.Remove(new Service() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
