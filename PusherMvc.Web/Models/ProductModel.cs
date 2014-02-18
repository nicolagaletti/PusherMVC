using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Models
{
    public class ProductModel
    {
        public string Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        //We ignore images for now
        //public ProductImage[] Images
        //{
        //    get;
        //    set;
        //}

        public int StockLevel
        {
            get;
            set;
        }

        public string StockStatus
        {
            get
            {
                return (StockLevel > 0 ? "In Stock" : "Out of Stock");
            }
        }
    }
}