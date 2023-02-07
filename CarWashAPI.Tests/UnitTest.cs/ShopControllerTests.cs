using CarWashAPI.Controllers.cs;
using CarWashAPI.DTOs.cs;
using CarWashAPI.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAPI.Tests.UnitTest.cs
{
    [TestClass]
    public class ShopControllerTests :BaseTests
    {
        [TestMethod]
        public async Task PutShop()
        {

            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            context.Users.Add(new CustomUser { Id = "AbCd!23", UserName = "Mmilos", Email = "milos@gmail.com",
                FirstName = "Milos", LastName = "Mijatovic" , Address = "Achhsahd"});

            context.Shops.Add(new Shop { ShopName = "Prasko", Address = "Lipetska", Id = 1, OwnerId = "AbCd!23",
                ClosingTime = 17, OpeningTime = 8 });
            context.SaveChanges();

            var context2 = BuildContext(databaseName);
            var controller = new ShopController(context2, mapper);
            controller.ControllerContext = BuildControllerContextWithDefaultUser();

            var shopCreationDTO = new ShopCreationDTO() { ShopName = "Prasko", Address = "Strumicka", OpeningTime = 10, ClosingTime = 18 };

            var id = 1;
            var response = await controller.PutShop(id, shopCreationDTO);

            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result.StatusCode);

            var context3 = BuildContext(databaseName);
            var exists = await context3.Shops.AnyAsync(x => x.ShopName == "Prasko");

            Assert.IsTrue(exists);
        }




      
    }
}
