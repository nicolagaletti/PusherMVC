using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.App_Start;
using PusherMvc.Web.Services;
using PusherRESTDotNet;
using PusherServer;
using PresenceChannelData = PusherRESTDotNet.Authentication.PresenceChannelData;

namespace PusherMvc.Web.Tests.Services
{
    [TestFixture]
    public class PusherServiceTest
    {
        private Product _availableProduct;
        private Product _unavailableProduct;
        private Mock<IPusher> _pusherMock;
        private Mock<IPusherProvider> _pusherProviderMock;

        #region SetUp / TearDown

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

            _pusherMock = new Mock<IPusher>();
            _pusherProviderMock = new Mock<IPusherProvider>();

            AutomapperConfig.RegisterMappings();
        }

        [TearDown]
        public void Dispose()
        {
            _availableProduct = null;
            _pusherMock = null;
            _pusherProviderMock = null;
        }

        #endregion

        #region Tests

        [Test]
        public void UpdateStock_AvailableProduct_CallsPusher()
        {
            //Arrange
            _pusherMock.Setup(m => m.Trigger(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Product>()));

            var pusherService = new PusherService(_pusherMock.Object, _pusherProviderMock.Object);

            //Act
            pusherService.UpdateStock(_availableProduct);

            //Assert
            _pusherMock.Verify(m => m.Trigger(It.IsAny<string>(), It.IsAny<string>(), _availableProduct), Times.Once);
        }

        [Test]
        public void UpdateStock_UnvailableProduct_CallsPusher()
        {
            //Arrange
            _pusherMock.Setup(m => m.Trigger(It.IsAny<string>(), It.IsAny<string>(), _unavailableProduct));

            var pusherService = new PusherService(_pusherMock.Object, _pusherProviderMock.Object);

            //Act
            pusherService.UpdateStock(_unavailableProduct);

            //Assert
            _pusherMock.Verify(m => m.Trigger(It.IsAny<string>(), It.IsAny<string>(), _unavailableProduct), Times.Never);
        }

        [Test]
        public void Auth_AnyInput_CallsPusherProvider()
        {
            //Arrange
            _pusherProviderMock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<PresenceChannelData>()));

            var pusherService = new PusherService(_pusherMock.Object, _pusherProviderMock.Object);

            //Act
            pusherService.Auth(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>());

            //Assert
            _pusherProviderMock.Verify(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<PresenceChannelData>()));
        }

        [Test]
        public void Auth_ValidInput_ReturnsString()
        {
            //Arrange
            const string channelName = "anychannel";
            const string socket = "anySocket";
            var channelData = new PresenceChannelData() { user_id = Guid.NewGuid().ToString() };
            const string expectedResult = "expectedResult";

            _pusherProviderMock.Setup(m => m.Authenticate(channelName, socket, channelData))
                .Returns(expectedResult);

            var pusherService = new PusherService(_pusherMock.Object, _pusherProviderMock.Object);

            //Act
            var result = pusherService.Auth(channelName, socket, "", new object());

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        #endregion
    }
}
