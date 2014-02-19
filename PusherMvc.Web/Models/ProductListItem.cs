using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Models
{
    public class ProductListItem
    {
        public string Id
        {
            get;
            set;
        }

        public string Header
        {
            get;
            set;
        }

        public int StockLevel
        {
            get;
            set;
        }

        public string Test { get; set; }

    }
}