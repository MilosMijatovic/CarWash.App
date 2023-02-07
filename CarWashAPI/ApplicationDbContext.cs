using CarWashAPI.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CarWashAPI
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext([NotNullAttribute]DbContextOptions options) : base(options) { }

        public DbSet<CustomUser> CustomUsers { get;  }

        public DbSet<Service> Services { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopsServices> ShopServices { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ShopsServices>().HasKey(x => new {x.ServiceId, x.ShopId});

            modelBuilder.Entity<ShopsServices>()
            .HasOne(cwo => cwo.Service)
            .WithMany(cwo => cwo.ShopsServices)
            .HasForeignKey(cwo => cwo.ServiceId);

            modelBuilder.Entity<ShopsServices>()
             .HasOne(cwo => cwo.Shop)
             .WithMany(cwo => cwo.ShopsServices)
             .HasForeignKey(cwo => cwo.ShopId);

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {


            var Megawash = new Shop()
            {
                Id = 1,
                ShopName = "Megawash",
                Address = "13 Augusta St.Yuma,AZ 85365",
                OwnerId = "8f773855-331b-4d9c-967f-891d19a03370",
                OpeningTime = 09,
                ClosingTime = 22

            };

            var Superwash = new Shop()
            {
                Id = 2,
                ShopName = "Superwash",
                Address = "6 White StreetBrick,NJ 08723",
                OwnerId = "38e4acff-3bb8-4cc7-97e4-3e5a6fa86695",
                OpeningTime = 8,
                ClosingTime = 22
            };

            var SparcleClean = new Shop()
            {
                Id = 3,
                ShopName = "Sparcle Clean",
                Address = "2 10th St.Bear,DE 19701",
                OwnerId = "5e8500b6-f72a-446c-9065-88d4a5e402e7", 
                OpeningTime = 8,
                ClosingTime = 20
            };

            var WashMeNow = new Shop()
            {
                Id = 4,
                ShopName = "Wash me Now",
                Address = "75 Hill Rd.Coventry,RI 02816",
                OwnerId = "142c4a35-3dd2-4013-ada9-64d11b55f848",
                OpeningTime = 7,
                ClosingTime = 19
            };

            var PlatinumAutoSpa = new Shop()
            {
                Id = 5,
                ShopName = "Platinum Auto Spa",
                Address = "911 Saxon St.Chaska,MN 55318",
                OwnerId = "2a222c3a-613e-4453-9359-748a2cc47780",
                OpeningTime = 9,
                ClosingTime = 17
            };

            modelBuilder.Entity<Shop>().HasData(new List<Shop>
            {
                Megawash, Superwash,SparcleClean, PlatinumAutoSpa,WashMeNow
            });





            var Regular = new Service()
            {
                Id = 1,  
                TypeOfService = "Regular",
                Price = "200"
                
            };

            var Extended = new Service()
            {
                Id = 2,
                TypeOfService = "Extended",
                Price = "300"

            };

            var Premium = new Service()
            {
                Id = 3,
                TypeOfService = "Premium",
                Price = "400"
            };

            modelBuilder.Entity<Service>().HasData(new List<Service>
            {
                Regular, Extended, Premium
            });


            modelBuilder.Entity<ShopsServices>().HasData(
                new List<ShopsServices>()
                {

                    new ShopsServices(){ShopId = Megawash.Id , ServiceId = Regular.Id},
                    new ShopsServices(){ShopId = Megawash.Id , ServiceId = Extended.Id},
                    new ShopsServices(){ShopId = Megawash.Id , ServiceId = Premium.Id},
                    new ShopsServices(){ShopId = Superwash.Id , ServiceId = Regular.Id},
                    new ShopsServices(){ShopId = Superwash.Id , ServiceId = Extended.Id},
                    new ShopsServices(){ShopId = Superwash.Id , ServiceId = Premium.Id},
                    new ShopsServices(){ShopId = SparcleClean.Id , ServiceId = Regular.Id},
                    new ShopsServices(){ShopId = SparcleClean.Id , ServiceId = Extended.Id},
                    new ShopsServices(){ShopId = SparcleClean.Id , ServiceId = Premium.Id},
                    new ShopsServices(){ShopId = PlatinumAutoSpa.Id , ServiceId = Regular.Id},
                    new ShopsServices(){ShopId = PlatinumAutoSpa.Id , ServiceId = Extended.Id},
                    new ShopsServices(){ShopId = PlatinumAutoSpa.Id , ServiceId = Premium.Id},
                    new ShopsServices(){ShopId = WashMeNow.Id , ServiceId = Regular.Id},
                    new ShopsServices(){ShopId = WashMeNow.Id , ServiceId = Extended.Id},
                    new ShopsServices(){ShopId = WashMeNow.Id , ServiceId = Premium.Id},

                });



            var hasher = new PasswordHasher<CustomUser>();
            var password = "ValueShore4!";
            var admin = new CustomUser()

            {
                Id = "24ab6a6c-14f1-4b49-8964-ecfcbce372a3",
                FirstName = "John",
                LastName = "Smith",
                Address = "9th Street",
                UserName = "jsmith",
                NormalizedUserName = "JSMITH",
                Email = "john.smith@gmail.com",
                NormalizedEmail = "JOHN.SMITH@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 582-282-2749"
            };

            var adminClaim = new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "24ab6a6c-14f1-4b49-8964-ecfcbce372a3",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Admin"
            };
            



            var Consumer1 = new CustomUser()

            {
                Id = "62291ee4-e619-4eef-8661-90abc336e188",
                FirstName = "Mike ",
                LastName ="Holand",
                Address = "15th Street",
                UserName = "mHoland",
                NormalizedUserName = "MHOLAND",
                Email = "mike.holand@gmail.com",
                NormalizedEmail = "MIKE.HOLAND@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 202-918-2132"
            };

            var Claim1 = new IdentityUserClaim<string>()
            {
                Id = 2,
                UserId = "62291ee4-e619-4eef-8661-90abc336e188",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Consumer"
            };



            var Consumer2 = new CustomUser()

            {
                Id = "b162a49a-b530-4db3-b43f-8ff0fa46b0fa",
                FirstName = "Stiv",
                LastName= "Nolan",
                Address = "2th Street",
                UserName = "snolan",
                NormalizedUserName = "SNOLAN",
                Email = "stiv.nolan@gmail.com",
                NormalizedEmail = "STIV.NOLAN@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 582-666-0863"
            };

            var Claim2 = new IdentityUserClaim<string>()
            {
                Id = 3,
                UserId = "b162a49a-b530-4db3-b43f-8ff0fa46b0fa",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Consumer"
            };



            var Consumer3 = new CustomUser()

            {
                Id = "88781281-86a7-434b-bae3-93ff20f6050c",
                FirstName = "Todd", 
                LastName = " Rodriguez ",
                Address = "18th Street",
                UserName = "trodriguez",
                NormalizedUserName = "TRODRIGUEZ",
                Email = "todd.rodriguez@gmail.com",
                NormalizedEmail = "TODD.RODRIGUEZ@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 212-467-8331"
            };

            var Claim3 = new IdentityUserClaim<string>()
            {
                Id = 4,
                UserId = "88781281-86a7-434b-bae3-93ff20f6050c",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Consumer"
            };


            var Consumer4 = new CustomUser()

            {
                Id = "2d99e43b-0faf-43ae-968f-d0bac3dba0c7",
                FirstName = "Ronald",
                LastName = "Park",
                Address = "50th Street",
                UserName = "ronaldpark",
                NormalizedUserName = "RONALDPARK",
                Email = "ronald.park@gmail.com",
                NormalizedEmail = "RONALD.PARK@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 505-644-4902"
            };

            var Claim4 = new IdentityUserClaim<string>()
            {
                Id = 5,
                UserId = "2d99e43b-0faf-43ae-968f-d0bac3dba0c7",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Consumer"
            };


            var Consumer5 = new CustomUser()

            {
                Id = "79da1f93-d724-4c18-88d2-4e7f62d44767",
                FirstName = "Elma",
                LastName= "Johns",
                Address = "50th Street",
                UserName = "elmajohns",
                NormalizedUserName = "ELMAJOHNS",
                Email = "elma.johns@gmail.com",
                NormalizedEmail = "ELMA.JOHNS@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 582-400-8337"
            };

            var Claim10 = new IdentityUserClaim<string>()
            {
                Id = 11,
                UserId = "79da1f93-d724-4c18-88d2-4e7f62d44767",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Consumer"
            };


            var Owner1 = new CustomUser()

            {
                Id = "8f773855-331b-4d9c-967f-891d19a03370",
                FirstName = "Raul",
                LastName = "Fisher",
                Address = "7955 Illinois St.Corona,NY 11368",
                UserName = "raulfisher",
                NormalizedUserName = "RAULFISHER",
                Email = "raul.fisher@gmail.com",
                NormalizedEmail = "RAUL.FISHER@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 202-981-8058"
            };

            var Claim5 = new IdentityUserClaim<string>()
            {
                Id = 6,
                UserId = "8f773855-331b-4d9c-967f-891d19a03370",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Owner"
            };


            var Owner2 = new CustomUser()

            {
                Id = "38e4acff-3bb8-4cc7-97e4-3e5a6fa86695",
                FirstName = "Shon", 
                LastName = "Connel",
                Address = "3 Madison Dr.New York, NY 10025",
                UserName = "shonconnel",
                NormalizedUserName = "SHONCONNEL",
                Email = "shon.connel@gmail.com",
                NormalizedEmail = "SHON.CONNEL@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 321-853-4272"
            };

            var Claim6 = new IdentityUserClaim<string>()
            {
                Id = 7,
                UserId = "38e4acff-3bb8-4cc7-97e4-3e5a6fa86695",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Owner"
            };


            var Owner3 = new CustomUser()

            {
                Id = "5e8500b6-f72a-446c-9065-88d4a5e402e7",
                FirstName = "Willa", 
                LastName = "Sosa",
                Address = "7955 Illinois St.Corona,NY 11368",
                UserName = "willasosa",
                NormalizedUserName = "WILLASOSA",
                Email = "willa.sosa@gmail.com",
                NormalizedEmail = "WILLA.SOSA@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 218-693-0149"
            };

            var Claim7 = new IdentityUserClaim<string>()
            {
                Id = 8,
                UserId = "5e8500b6-f72a-446c-9065-88d4a5e402e7",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Owner"
            };




            var Owner4 = new CustomUser()

            {
                Id = "142c4a35-3dd2-4013-ada9-64d11b55f848",
                FirstName = "Stacy",
                LastName = "Reid",
                Address = "62 East St.Bronx,NY 10452",
                UserName = "stacyreid",
                NormalizedUserName = "STACYREID",
                Email = "stacy.reid@gmail.com",
                NormalizedEmail = "STACY.REID@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 301-252-1209"
            };

            var Claim8 = new IdentityUserClaim<string>()
            {
                Id = 9,
                UserId = "142c4a35-3dd2-4013-ada9-64d11b55f848",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Owner"
            };


            var Owner5 = new CustomUser()

            {
                Id = "2a222c3a-613e-4453-9359-748a2cc47780",
                FirstName = "Alden",
                LastName =  "Butler",
                Address = "29 Euclid St.Bronx,NY 10451",
                UserName = "aldenbutler",
                NormalizedUserName = "ALDENBUTLER",
                Email = "alden.butler@gmail.com",
                NormalizedEmail = "ALDEN.BUTLER@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                PhoneNumber = "+1 206-598-8183"
            };

            var Claim9 = new IdentityUserClaim<string>()
            {
                Id = 10,
                UserId = "2a222c3a-613e-4453-9359-748a2cc47780",
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Owner"
            };


            var users = new List<CustomUser>()
            {
                admin,Consumer1,Consumer2,Consumer3,Consumer4,Consumer5,Owner1,Owner2,Owner3,Owner4,Owner5
            };


            var claims = new List<IdentityUserClaim<string>>()
            {
                adminClaim, Claim1,Claim2,Claim3,Claim4,Claim5,Claim6,Claim7,Claim8,Claim9,Claim10
            };




            modelBuilder.Entity<CustomUser>().HasData(users);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(claims);





            var booking1 = new Booking()
            {
                Id = 1,
                Reserved = new DateTime(2022, 08, 30, 11,00,00),
                ConsumerId = "62291ee4-e619-4eef-8661-90abc336e188",
                ShopId = 1,
                ServiceId = 1,
            };

            var booking2 = new Booking()
            {
                Id = 2,
                Reserved = new DateTime(2022, 08, 30, 12, 00, 00),
                ConsumerId = "b162a49a-b530-4db3-b43f-8ff0fa46b0fa",
                ShopId = 2,
                ServiceId = 1,
            };

            var booking3 = new Booking()
            {
                Id = 3,
                Reserved = new DateTime(2022, 08, 30, 10, 00, 00),
                ConsumerId = "88781281-86a7-434b-bae3-93ff20f6050c",
                ShopId = 3,
                ServiceId = 3,
            };

            var booking4 = new Booking()
            {
                Id = 4,
                Reserved = new DateTime(2022, 08, 30, 12, 00, 00),
                ConsumerId = "2d99e43b-0faf-43ae-968f-d0bac3dba0c7",
                ShopId = 4,
                ServiceId = 2,
            };

            var booking5 = new Booking()
            {
                Id = 5,
                Reserved = new DateTime(2022, 08, 30, 15, 00, 00),
                ConsumerId = "79da1f93-d724-4c18-88d2-4e7f62d44767",
                ShopId = 5,
                ServiceId = 3,
            };

            var booking6 = new Booking()
            {
                Id = 6,
                Reserved = new DateTime(2022, 09, 02, 15, 00, 00),
                ConsumerId = "b162a49a-b530-4db3-b43f-8ff0fa46b0fa",
                ShopId = 4,
                ServiceId = 3,
            };

            var booking7 = new Booking()
            {
                Id = 7,
                Reserved = new DateTime(2022, 09, 05, 17, 00, 00),
                ConsumerId = "2d99e43b-0faf-43ae-968f-d0bac3dba0c7",
                ShopId = 1,
                ServiceId = 3,
            };

            var booking8 = new Booking()
            {
                Id = 8,
                Reserved = new DateTime(2022, 09, 06, 18, 00, 00),
                ConsumerId = "62291ee4-e619-4eef-8661-90abc336e188",
                ShopId = 2,
                ServiceId = 3,
            };


            modelBuilder.Entity<Booking>().HasData(new List<Booking>
            {
                booking1,booking2,booking3,booking4,booking5,booking6,booking7,booking8

            });
        }

        
    }
}
