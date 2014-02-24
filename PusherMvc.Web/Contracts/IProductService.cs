using PusherMvc.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Contracts
{
    public interface IProductService
    {
        Product[] ListProducts();
        bool BuyProduct(string productId);
        Product GetProduct(string productId);
        void CreateProduct(Product product);
    }
}