using CarWashAPI.Entites;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarWashAPI.DTOs.cs
{
    public class BookingCreationDTO 
    {
        
        [Required]
        
        public DateTime Reserved { get; set; }

        public int ShopId { get; set; }
        public int ServiceId { get; set; }

        //izvor informacija za shopName i serviceName (prikrivena radnja)
        [JsonIgnore]
        public ShopsServices? ShopService { get; set; }




    }
}
