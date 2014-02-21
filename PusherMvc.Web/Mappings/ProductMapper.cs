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

            Mapper.CreateMap<Product, ProductListItemViewModel>()
                .ForMember(vm => vm.Header, opt => opt.MapFrom(p => p.Title));
                //.ForMember(vm => vm.Test, opt => opt.Ignore());

            Mapper.CreateMap<AddProductViewModel, Product>()
                .ForMember(p => p.StockStatus, opt => opt.Ignore());
            Mapper.CreateMap<Product, AddProductViewModel>();
                

            Mapper.CreateMap<ProductListItemViewModel, Product>()
                .ForMember(p => p.Title, opt => opt.MapFrom(vm => vm.Header))
                .ForMember(p => p.Description, opt => opt.Ignore());
        }
    }
}