using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.Entites
{
    public class Shop
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string ShopName { get; set; }    
        public string Address { get; set; }
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }
        public string OwnerId { get; set; }
        public CustomUser Owner { get; set; }

        public List<ShopsServices> ShopsServices { get; set; } 
        
    }
}
