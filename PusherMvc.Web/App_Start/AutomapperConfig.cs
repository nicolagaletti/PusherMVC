using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PusherMvc.Web.Mappings;

namespace PusherMvc.Web.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(Mappings);
            Mapper.AssertConfigurationIsValid();
        }

        private static void Mappings(IConfiguration configuration)
        {
            configuration.AddProfile<ProductMapper>();
        }
    }
}