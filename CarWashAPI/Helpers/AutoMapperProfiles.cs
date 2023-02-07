using AutoMapper;
using CarWashAPI.DTOs.cs;
using CarWashAPI.Entites;
using Microsoft.AspNetCore.Identity;
using System.Linq;
namespace CarWashAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CustomUser, CustomUserDTO>().ReverseMap();

           

            CreateMap<Booking, BookingDTO>()
                .ForMember(x => x.ServiceName , options => options.MapFrom(x => x.Service.TypeOfService))
                .ForMember(x => x.ShopName, options => options.MapFrom(x => x.Shop.ShopName))
                .ReverseMap();

            CreateMap<BookingCreationDTO, Booking>()
                .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.ConsumerId, options => options.Ignore())
                .ForMember(x => x.Consumer, options => options.Ignore())
                .ForMember(x => x.Service, options => options.Ignore())
                .ForMember(x => x.Shop, options => options.Ignore())
                .ForMember(x => x.Created, options => options.Ignore());
      

            CreateMap<Shop , ShopDTO>().ReverseMap();

            CreateMap<ShopCreationDTO, Shop>()
                .ForMember(x => x.Id , options => options.Ignore())
                .ForMember(x => x.OwnerId, options => options.Ignore())
                .ForMember(x => x.Owner, options => options.Ignore())
                .ForMember(x => x.ShopsServices, options => options.Ignore());


            CreateMap<Shop, ShopDetailsDTO>()
                .ForMember(x => x.Services, options => options.MapFrom(Service2ServiceDTO));
               

            CreateMap<Service , ServiceDTO>().ReverseMap();

            CreateMap<ServiceCreationDTO, Service>()
                .ForMember(x => x.ShopsServices, options => options.Ignore());
          
                    

            CreateMap<IdentityUser, UserDTO>()
                     .ForMember(x => x.EmailAddress, options => options.MapFrom(x => x.Email))
                     .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id));

        }


        private List<ServiceDTO> Service2ServiceDTO(Shop source, ShopDetailsDTO destination)
        {
            var serviceDTOs = new List<ServiceDTO>();

            foreach(var item in source.ShopsServices)
            {
                serviceDTOs.Add(new ServiceDTO()
                {
                    Id = item.ServiceId,
                    TypeOfService = item.Service.TypeOfService,
                    Price = item.Service.Price
                });
            }

            return serviceDTOs;
        }


        //private List<ShopsServices> MapShopsServices(ShopCreationDTO shopCreationDTO, Shop shop)
        //{
        //    var result = new List<ShopsServices>();
        //    foreach (var id in shopCreationDTO.ServicesId) // Proveriti ovo
        //    {
        //        result.Add(new ShopsServices() { ServiceId = id }); 
        //    }
        //    return result;
        //}
        //private List<ShopDTO>MapShopsServices(Shop shop , ShopDetailsDTO shopDetailsDTO)
        //{
        //    var result = new List<ShopDTO>();
        //    foreach(var shopservice in shop.ShopsServices)
        //    {
        //        result.Add(new ShopDTO() { Id = shopservice.ShopId, ShopName = shopservice.Shop.ShopName });
        //    }
        //    return result;
        //}
    }
}
