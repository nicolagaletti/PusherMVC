using NUnit.Framework;
using PusherMvc.Data.Entities;

namespace PusherMvc.Data.Tests.Entities
{
    [TestFixture]
    public class ProductTest
    {
        private Product _product;

        #region Tests

        [Test]
        public void Product_Available_ReturnInStock()
        {
            //Arrange
            _product = new Product()
            {
                StockLevel = 1
            };

            //Act

            //Assert
            Assert.AreEqual(_product.StockStatus, "In Stock");
        }

        [Test]
        public void Product_Unavailable_ReturnOutOfStock()
        {
            //Arrange
            _product = new Product()
            {
                StockLevel = 0
            };

            //Act

            //Assert
            Assert.AreEqual(_product.StockStatus, "Out of Stock");
        }

        #endregion
    }
}
