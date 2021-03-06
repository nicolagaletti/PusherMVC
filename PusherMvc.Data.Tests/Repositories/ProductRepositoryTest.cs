using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PusherMvc.Data.Entities;
using PusherMvc.Data.Tests.Fakes;
using Raven.Client;
using Raven.Client.Linq;
using PusherMvc.Data.Repositories;

namespace PusherMvc.Data.Tests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTest
    {
        private Product _product;
        private Mock<IDocumentSession> _documentSessionMock;
        
        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            _product = new Product
            {
                Description = "Product_Available_ReturnInStock description",
                Id = "1",
                StockLevel = 10,
                Title = "Product_Available_ReturnInStock Title"
            };

            _documentSessionMock = new Mock<IDocumentSession>();
        }

        [TearDown]
        public void Dispose()
        {
            _product = null;
        }

        #endregion

        #region Tests

        [Test]
        public void CreateProduct_ValidProduct_NoExceptionsThrown()
        {
            //Arrange
            object expectedProduct = null;

            _documentSessionMock.Setup(
                m => m.Store(_product))
                .Callback<object>(p => expectedProduct = p);

            var productRepository = new Data.Repositories.ProductRepository(_documentSessionMock.Object);

            //Act
            productRepository.CreateProduct(_product);

            //Assert
            Assert.AreEqual(_product, expectedProduct);
        }

        [Test]
        public void ListProducts_NoInput_ReturnsListOfProducts()
        {
            //Arrange
            var productList = new List<Product> {_product};
            var expectedProducts = new FakeRavenQueryable<Product>(productList.AsQueryable()); 
            
            _documentSessionMock.Setup(m => m.Query<Product>())
                .Returns(expectedProducts);

            var productRepository = new Data.Repositories.ProductRepository(_documentSessionMock.Object);

            //Act
            Product[] actualProducts = productRepository.ListProducts();

            //Assert
            Assert.AreEqual(expectedProducts, actualProducts);
        }

        [Test]
        public void GetProductById_ValidId_ReturnsProduct()
        {
            //Arrange
            _documentSessionMock.Setup(m => m.Load<Product>("1"))
                .Returns(_product);
 
            var productRepository = new ProductRepository(_documentSessionMock.Object);

            //Act
            var actualProduct = productRepository.GetProductById("1");

            //Assert
            Assert.AreEqual(_product, actualProduct);
        }

        [Test]
        public void GetProductById_InvalidId_ReturnsNull()
        {
            //Arrange
            _documentSessionMock.Setup(m => m.Load<Product>("1")).Returns(_product);

            var productRepository = new ProductRepository(_documentSessionMock.Object);

            //Act
            var actualProduct = productRepository.GetProductById("2");

            //Assert
            Assert.IsNull(actualProduct);
        }

        #endregion
    }
}
