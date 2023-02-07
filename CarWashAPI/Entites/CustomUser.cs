
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.Entites
{
    public class CustomUser : IdentityUser
    {

        [Required]
        [StringLength(80)]

        public string FirstName { get; set; }

        
        public string LastName { get; set; }

       
        [Required]
        public string Address { get; set; }

        public List<Shop>? MyShops { get; set; }
        public List<Booking>? MyBookings { get; set; }
    }
}
