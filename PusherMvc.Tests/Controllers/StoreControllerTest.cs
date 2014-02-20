using Moq;
using NUnit.Framework;
using PusherMvc.Data.Contracts;
using PusherMvc.Web.Controllers;
using PusherMvc.Web.Models;

namespace PusherMvc.Tests.Controllers
{
    [TestFixture]
    public class StoreControllerTest
    {
        private readonly AddProductViewModel _addProductView = new AddProductViewModel
            {
                Id = "ProductModels-2",
                Title = "Test _addProductView",
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
