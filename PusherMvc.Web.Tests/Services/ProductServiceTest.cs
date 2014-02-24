using Moq;
using NUnit.Framework;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.App_Start;
using PusherMvc.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Web.Tests.Services
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Product _availableProduct;
        private Product _unavailableProduct;

        private Mock<IProductRepository> _productRepositoryMock;
        
        [SetUp]
        public void Init()
        {
            _availableProduct = new Product
            {
                Description = "Test description",
                Id = "1",
                StockLevel = 10,
                Title = "Test Title"
            };

            _unavailableProduct = new Product
            {
                Description = "Test description",
                Id = "1",
                StockLevel = 0,
                Title = "Test Title"
            };

            _productRepositoryMock = new Mock<IProductRepository>();

            AutomapperConfig.RegisterMappings();
        }
        
        [Test]
        public void BuyProduct_ValidId_VerifyRepositoryIsCalled()
        {
            //Arrange
            _productRepositoryMock.Setup(m => m.GetProductById(It.IsAny<string>()))
                .Returns(_availableProduct);

            var productService = new ProductService(_productRepositoryMock.Object);
            
            //Act
            var bought = productService.BuyProduct(_availableProduct.Id);

            //Assert
            _productRepositoryMock.Verify(v => v.GetProductById(_availableProduct.Id), Times.Once);
        }

        [Test]
        public void BuyProduct_AvailableProduct_ReturnsTrue()
        {
            //Arrange
            int quantityAfterBuy = _availableProduct.StockLevel - 1;

            _productRepositoryMock.Setup(m => m.GetProductById(_availableProduct.Id))
                .Returns(_availableProduct);

            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = productService.BuyProduct(_availableProduct.Id);

            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void BuyProduct_AvailableProduct_DecreaseStock()
        {
            //Arrange
            int quantityAfterBuy = _availableProduct.StockLevel - 1;
            
            _productRepositoryMock.Setup(m => m.GetProductById(_availableProduct.Id))
                .Returns(_availableProduct);

            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = productService.BuyProduct(_availableProduct.Id);

            //Assert
            Assert.AreEqual(quantityAfterBuy, _availableProduct.StockLevel);
        }

        [Test]
        public void BuyProduct_UnavailableProduct_ReturnsFalse()
        {
            //Arrange
            int quantityAfterBuy = _unavailableProduct.StockLevel - 1;

            _productRepositoryMock.Setup(m => m.GetProductById(_unavailableProduct.Id))
                .Returns(_unavailableProduct);

            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = productService.BuyProduct(_unavailableProduct.Id);

            //Assert
            Assert.IsFalse(result);
        }

        public void BuyProduct_NullProduct_ReturnsFalse()
        {
            //Arrange
            _productRepositoryMock.Setup(m => m.GetProductById(It.IsAny<string>()))
                .Returns<Product>(null);

            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = productService.BuyProduct("1");

            //Assert
            Assert.IsFalse(result);
        }
    }
}
