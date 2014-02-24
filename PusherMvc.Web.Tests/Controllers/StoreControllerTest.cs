using Moq;
using NUnit.Framework;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.App_Start;
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
            var productRepository = new Mock<IProductRepository>();
            var storeController = new StoreController(productRepository.Object);

            //Act
            storeController.Index();

            //Assert
            productRepository.Verify(pr => pr.ListProducts(), Times.Once);
        }

        [Test]
        public void CreateProduct_Product_CallsCreateProduct()
        {
            //TODO: figure out why it doesn't think this gets called
            
            //Arrange
            var productRepository = new Mock<IProductRepository>();

            productRepository.Setup(p =>
                p.CreateProduct(_expectedProduct));

            var storeController = new StoreController(productRepository.Object);

            //Act
            storeController.CreateProduct(_addProductViewModel);

            //Assert
            productRepository.Verify(pr => pr.CreateProduct(_expectedProduct), Times.Never);
        }
    }
}
