namespace CarWashAPI.Entites
{
    public class ShopsServices
    { 
        public int ServiceId { get; set; }  
        public int ShopId { get; set; } 

        public Shop Shop { get; set; }
        public Service Service { get; set; }
        
       
    }
}
