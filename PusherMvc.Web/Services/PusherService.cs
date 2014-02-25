using PusherMvc.Data.Entities;
using PusherMvc.Web.Contracts;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Services
{
    public class PusherService : IPusherService
    {
        private IPusher _pusher;

        public PusherService(IPusher pusher)
        {
            _pusher = pusher;
        }
        
        public void UpdateStock(Product product)
        {
            if (product.StockLevel > 0)
            {
                _pusher.Trigger(String.Format("product-{0}", product.Id), "StockUpdated", product);
            }
        }
    }
}