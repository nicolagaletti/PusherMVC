using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Web.Contracts
{
    public interface IPusherService
    {
        void UpdateStock(string productId);
    }
}
