using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Data.Entities
{
    public class Product
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

        public string StockStatus
        {
            get
            {
                return (StockLevel > 0 ? "In Stock" : "Out of Stock");
            }
        }
    }
}
