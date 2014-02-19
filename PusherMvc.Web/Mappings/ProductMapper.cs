using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PusherMvc.Data.Entities;
using PusherMvc.Web.Models;

namespace PusherMvc.Web.Mappings
{
    public class ProductMapper : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<Product, ProductListItem>()
                .ForMember(vm => vm.Header, opt => opt.MapFrom(p => p.Title))
                .ForMember(vm => vm.Test, opt => opt.Ignore());

            Mapper.CreateMap<ProductModel, Product>();
        }
    }
}