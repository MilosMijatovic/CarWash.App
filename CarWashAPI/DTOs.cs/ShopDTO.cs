using CarWashAPI.Entites;

namespace CarWashAPI.DTOs.cs
{
    public class ShopDTO
    {
        public int Id { get; set; }
  
        public string ShopName { get; set; }
        public string Address { get; set; }

        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }

    }
}
