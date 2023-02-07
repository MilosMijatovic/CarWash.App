using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTOs.cs
{
    public class LogInDTO
    {
       
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
        

        [Required]
        public string Password { get; set; }
    }
}
