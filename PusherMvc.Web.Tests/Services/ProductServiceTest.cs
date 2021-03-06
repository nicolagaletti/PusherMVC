﻿using Moq;
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
                Description = "UpdateStock_AvailableProduct_CallsPusher description",
                Id = "1",
                StockLevel = 10,
                Title = "UpdateStock_AvailableProduct_CallsPusher Title"
            };

            _unavailableProduct = new Product
            {
                Description = "UpdateStock_AvailableProduct_CallsPusher description",
                Id = "1",
                StockLevel = 0,
                Title = "UpdateStock_AvailableProduct_CallsPusher Title"
            };

            _productRepositoryMock = new Mock<IProductRepository>();

            AutomapperConfig.RegisterMappings();
        }

        [TearDown]
        public void Dispose()
        {
            _availableProduct = null;
            _unavailableProduct = null;
            _productRepositoryMock = null;
        }

        [Test]
        public void BuyProduct_NullProductId_ReturnsFalse()
        {
            _productRepositoryMock.Setup(m => m.GetProductById(null))
                .Returns<Product>(null);

            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = productService.BuyProduct(null);

            //Assert
            {
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void BuyProduct_EmptyProductId_ReturnsFalse()
        {
            _productRepositoryMock.Setup(m => m.GetProductById(""))
                .Returns<Product>(null);

            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = productService.BuyProduct("");

            //Assert
            {
                Assert.IsFalse(result);
            }
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

        [Test]
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

        [Test]
        public void CreateProduct_AnyProduct_CallsProductRepositoryCreateProduct()
        {
            //Arrange
            _productRepositoryMock.Setup(m => m.CreateProduct(It.IsAny<Product>()));
            var productService = new ProductService(_productRepositoryMock.Object);
            
            //Act
            productService.CreateProduct(_availableProduct);

            //Assert
            _productRepositoryMock.Verify(v => v.CreateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void ListProducts_Nothing_CallsProductRepositoryListProducts()
        {
            //Arrange
            _productRepositoryMock.Setup(m => m.ListProducts())
                .Returns(It.IsAny<Product[]>());
            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            productService.ListProducts();

            //Assert
            _productRepositoryMock.Verify(v => v.ListProducts(), Times.Once);
        }

        [Test]
        public void GetProduct_Nothing_CallsProductRepositoryGetProduct()
        {
            //Arrange
            _productRepositoryMock.Setup(m => m.GetProductById(It.IsAny<string>()))
                .Returns(It.IsAny<Product>());
            var productService = new ProductService(_productRepositoryMock.Object);

            //Act
            productService.GetProduct("");

            //Assert
            _productRepositoryMock.Verify(v => v.GetProductById(It.IsAny<string>()), Times.Once);
        }
    }
}
