using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        private static WebApplicationFactory<Program>? factory=null;
        private static IServiceScopeFactory? scopeFactory=null;
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            factory = new CustomWebApplicationFactory();
            scopeFactory=factory.Services.GetService<IServiceScopeFactory>();
        }
        [TestMethod]
        public async Task RegisterCategory_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope=scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();
            //Arrage
            var request =new CategoryRequestDto{
                Description="",
                Name="",
                State=1
            };
            var expected = ReplyMessage.MESSAGE_VALIDATE;
            //Act
            var result = await context!.RegisterCategory(request);
            var current = result.Message;
            //Assert
            Assert.AreEqual(expected, current);
        }
        [TestMethod]
        public async Task RegisterCategory_WhenSendingCorrectValues_RegisteredSuccesfully()
        {
            using var scope = scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();
            //Arrage
            var request = new CategoryRequestDto
            {
                Description = "Nuevo Descripcion",
                Name = "Nuevo Regitro",
                State = 1
            };
            var expected = ReplyMessage.MESSAGE_SAVE;
            //Act
            var result = await context!.RegisterCategory(request);
            var current = result.Message;
            //Assert
            Assert.AreEqual(expected, current);
        }
    }
}
