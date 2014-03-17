using PusherMvc.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Data.Contracts
{
    public interface IUserBuilder
    {
        User Build();
    }
}
