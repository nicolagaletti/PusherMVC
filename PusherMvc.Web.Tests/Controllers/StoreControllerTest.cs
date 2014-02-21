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
                Title = "Test Title",
                Description = null,
                StockLevel = 10
            };

        private readonly Product _expectedProduct = new Product
        {
            Id = null,
            Title = "Test Title",
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
            StoreController storeController = new StoreController(productRepository.Object);

            //Act
            storeController.Index();

            //Assert
            productRepository.Verify(pr => pr.ListProducts(), Times.Once);
        }

        [Test]
        public void CreateProduct_Product_CallsCreateProduct()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();

            productRepository.Setup(p =>
                p.CreateProduct(_expectedProduct));

            StoreController storeController = new StoreController(productRepository.Object);

            //Act
            storeController.CreateProduct(_addProductViewModel);

            //Assert
            productRepository.Verify(pr => pr.CreateProduct(_expectedProduct), Times.Once);
        }
    }


}
