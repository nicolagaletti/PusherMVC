using Moq;
using NUnit.Framework;
using PusherMvc.Web.Contracts;
using PusherMvc.Web.Controllers;
using PusherMvc.Web.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Tests.Controllers
{
    [TestFixture]
    public class StoreControllerTest
    {
        private readonly ProductModel product = new ProductModel
            {
                Id = "ProductModels-2",
                Title = "Test product",
                Description = "Test description",
                StockLevel = 10
            };

        [Test]
        public void Index_NoInput_CallsListProducts()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            StoreController storeController = new StoreController(productRepository.Object);

            //Act
            storeController.Index();

            //Assert
            productRepository.Verify(pr => pr.ListProducts(), Times.Once);
        }
    }


}
