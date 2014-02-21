using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Models
{
    public class AddProductViewModel
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

        public int StockLevel
        {
            get;
            set;
        }
    }
}