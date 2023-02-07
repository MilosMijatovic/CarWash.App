using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.Entites
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        
        public DateTime Reserved { get; set; }   
        public string? ConsumerId { get; set; }
        public CustomUser? Consumer { get; set; }
        public int ServiceId { get; set; }  
        public Service Service { get; set; } 
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;


    }
}
