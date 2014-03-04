using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PusherMvc.Data.Entities;

namespace PusherMvc.Web.Contracts
{
    public interface IPusherService
    {
        void UpdateStock(Product product);
        string Auth(string channelName, string socketId);
    }
}
