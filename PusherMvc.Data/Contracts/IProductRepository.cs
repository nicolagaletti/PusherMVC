using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PusherMvc.Data.Entities;

namespace PusherMvc.Data.Contracts
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product[] ListProducts();
    }
}
