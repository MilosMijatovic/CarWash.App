using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWashAPI.Controllers.cs;
using CarWashAPI.DTOs.cs;
using CarWashAPI.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarWashAPI.Tests.UnitTest.cs
{
    [TestClass]
    public class BookingControllerTests : BaseTests
    {
        [TestMethod]
        public async Task GetAllShops()
        {
            //Preparation
            var databaseName = Guid.NewGuid().ToString();   
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            context.Shops.Add(new Shop() { ShopName = "Shop1" , Address = "Markovacka", OwnerId = "Abcd1"});
            context.Shops.Add(new Shop() { ShopName = "Shop2" , Address = "Smiljaniceva" , OwnerId = "Abcd2" });
            context.SaveChanges();


            var context2 = BuildContext(databaseName);

            //Testing

            var controller = new BookingController(context2, mapper);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var pagination = new PaginationDTO() { Page = 1, RecordsPerPage = 50 };
            var response = await controller.Get(pagination);



            //Verification
            var shops = response.Value;
            Assert.AreEqual(2, shops.Count);

        }
        [TestMethod]
        public async Task GetShopByIdDoesNotExist()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            var controller = new BookingController(context , mapper);
            var response = await controller.Get(1);
            var result = response.Result as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public async Task CreateBooking()
        {
            var datebaseName = Guid.NewGuid().ToString();
            var context = BuildContext(datebaseName);
            var mapper = BuildMap();

            var newBooking = new BookingCreationDTO() { Reserved = DateTime.Now, ServiceId = 1, ShopId = 1 };

            context.Users.Add(new CustomUser() { Id = "Abcd1", FirstName="Milos", LastName= "Mijatovic", Address = "Markovacka", Email = "milos@gmail.com", UserName = "Mmilos"});
            context.Shops.Add(new Shop() {Id=1, ShopName = "Shop1", Address = "Markovacka", OwnerId = "Abcd1" });
            context.Services.Add(new Service { Id = 1, Price = "200", TypeOfService = "Premium" });
            context.ShopServices.Add(new ShopsServices { ShopId = 1 , ServiceId = 1});


            context.SaveChanges();

            var context2 = BuildContext(datebaseName);
            var controller = new BookingController(context2, mapper);
            controller.ControllerContext = BuildControllerContextWithDefaultUser();

            var response = await controller.PostReservation(newBooking);
            var result = response as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            
            var context3 = BuildContext(datebaseName);
            var count = await context3.Bookings.CountAsync();
            Assert.AreEqual(1, count);

        }

        [TestMethod]

        public async Task CancelBookingNotFound()
        {
            var datebaseName = Guid.NewGuid().ToString();
            var context = BuildContext(datebaseName);
            var mapper = BuildMap();


            var controller = new BookingController(context, mapper);
            controller.ControllerContext = BuildControllerContextWithDefaultUser();

            var response = await controller.Delete(1);
            var result = response as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);
           

        }

        [TestMethod]
        public async Task CancelBookingNoContent()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            context.Users.Add(new CustomUser() { Id = "Abcd1", FirstName = "Milos", LastName = "Mijatovic", Address = "Markovacka",
                Email = "milos@gmail.com", UserName = "Mmilos" });
            context.Bookings.Add(new Booking() { Id = 5, Reserved = DateTime.Now.AddHours(1), ShopId = 2 , ServiceId = 3,
                ConsumerId = "Abcd1", Created = DateTime.Today});
            context.SaveChanges();

            var context2 = BuildContext(databaseName);

            var controller = new BookingController(context2, mapper);
            controller.ControllerContext = BuildControllerContextWithDefaultUser();

            var response = await controller.Delete(5);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204 , result.StatusCode);

            var context3 = BuildContext(databaseName);
            var exists = await context3.Bookings.AnyAsync();
            Assert.IsFalse(exists);

        }


        [TestMethod]
        public async Task CancelBookingBadRequest()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();
            string message = "To late to delete";

            context.Users.Add(new CustomUser()
            {
                Id = "Abcd1",
                FirstName = "Milos",
                LastName = "Mijatovic",
                Address = "Markovacka",
                Email = "milos@gmail.com",
                UserName = "Mmilos"
            });
            context.Bookings.Add(new Booking()
            {
                Id = 5,
                Reserved = DateTime.Now,
                ShopId = 2,
                ServiceId = 3,
                ConsumerId = "Abcd1",
                Created = DateTime.Today
            });
            context.SaveChanges();

            var context2 = BuildContext(databaseName);

            var controller = new BookingController(context2, mapper);
            controller.ControllerContext = BuildControllerContextWithDefaultUser();

            var response = await controller.Delete(5);
            var result = response as BadRequestObjectResult;
            Assert.AreEqual(400, result.StatusCode);

            var result2 = response as BadRequestObjectResult;
            Assert.AreEqual(message, result2.Value);

            var context3 = BuildContext(databaseName);
            var exists = await context3.Bookings.AnyAsync();
            Assert.IsTrue(exists);
            

        }
    }

} 
