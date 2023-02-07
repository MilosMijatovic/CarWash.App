using System.ComponentModel.DataAnnotations;
using static CarWashAPI.Helpers.Enums;

namespace CarWashAPI.DTOs.cs
{
    public class UserInfo : LogInDTO
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }


        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }    
        public string Address { get; set; }
        public bool IsOwner { get; set; }
    }
}
