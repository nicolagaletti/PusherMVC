using PusherMvc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Web.Contracts
{
    public interface IProductRepository
    {
        void CreateProduct(ProductModel product);
        ProductModel[] ListProducts();
    }
}
