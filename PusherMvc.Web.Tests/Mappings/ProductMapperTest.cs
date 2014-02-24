using AutoMapper;
using NUnit.Framework;
using PusherMvc.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Web.Tests.Mappings
{
    [TestFixture]
    public class ProductMapperTest
    {
        [Test]
        public void ProductMapper_Configuration_IsValid()
        {
            Mapper.Initialize(m => m.AddProfile<ProductMapper>());
            Mapper.AssertConfigurationIsValid();
        }
    }
}
