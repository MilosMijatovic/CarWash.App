using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTOs.cs
{
    public class ServiceCreationDTO
    {

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeOfService { get; set; }

        public string Price { get; set; }

     
    }
}
