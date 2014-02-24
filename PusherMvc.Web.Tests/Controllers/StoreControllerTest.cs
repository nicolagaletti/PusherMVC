using Moq;
using NUnit.Framework;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.App_Start;
using PusherMvc.Web.Contracts;
using PusherMvc.Web.Controllers;
using PusherMvc.Web.Models;

namespace PusherMvc.Web.Tests.Controllers
{
    [TestFixture]
    public class StoreControllerTest
    {
        private readonly AddProductViewModel _addProductViewModel = new AddProductViewModel
            {
                Id = null,
                Title = "UpdateStock_AvailableProduct_CallsPusher Title",
                Description = null,
                StockLevel = 10
            };

        private readonly Product _expectedProduct = new Product
        {
            Id = null,
            Title = "UpdateStock_AvailableProduct_CallsPusher Title",
            Description = null,
            StockLevel = 10
        };

        [SetUp]
        public void SetUp()
        {
            AutomapperConfig.RegisterMappings();
        }

        [Test]
        public void Index_NoInput_CallsListProducts()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            var pusherService = new Mock<IPusherService>();
            var storeController = new StoreController(productService.Object, pusherService.Object);

            //Act
            storeController.Index();

            //Assert
            productService.Verify(pr => pr.ListProducts(), Times.Once);
        }

        [Test]
        public void CreateProduct_Product_CallsCreateProduct()
        {
            //Arrange
            var productRepository = new Mock<IProductService>();

            productRepository.Setup(p =>
                p.CreateProduct(_expectedProduct));

            var pusherService = new Mock<IPusherService>();

            var storeController = new StoreController(productRepository.Object, pusherService.Object);

            //Act
            storeController.CreateProduct(_addProductViewModel);

            //Assert
            productRepository.Verify(pr => pr.CreateProduct(_expectedProduct), Times.Never);
        }
    }
}
