using AutoMapper;
using CarWashAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAPI.Tests.UnitTest.cs
{
    [TestClass]
    public class AutoMapperConfigurationTests
    {
        [TestMethod]
        public void AssertConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
            configuration.AssertConfigurationIsValid();
        }
    }
}
