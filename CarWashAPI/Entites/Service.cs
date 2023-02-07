using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.Entites
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfService { get; set; }

        public string Price { get; set; }

        public List<ShopsServices> ShopsServices { get; set; }
    }
}
