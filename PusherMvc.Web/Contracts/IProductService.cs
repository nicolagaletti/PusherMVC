using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Contracts
{
    public interface IProductService
    {
        bool BuyProduct(string productId);
    }
}