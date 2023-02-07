using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTOs.cs
{
    public class ServiceDTO
    {
        public int Id { get; set; }

    
        public string TypeOfService { get; set; }

        public string Price { get; set; }


    }
}
