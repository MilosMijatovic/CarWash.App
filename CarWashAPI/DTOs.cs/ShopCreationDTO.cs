
using System.ComponentModel.DataAnnotations;


namespace CarWashAPI.DTOs.cs
{

    public class ShopCreationDTO 
    {
       
        public string ShopName { get; set; }
        public string Address { get; set; }

        [Required]
        [Range(0,24)]
        public int OpeningTime { get; set; }
        [Required]
        [Range(0,24)]
        public int ClosingTime { get; set; }

       
    }
}
