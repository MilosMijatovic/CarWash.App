using CarWashAPI.Entites;

namespace CarWashAPI.DTOs.cs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime Reserved { get; set; }
        public string ServiceName { get; set; }
        public string ShopName { get; set; }
    }
}
