using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Data.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Timestamp { get; private set; }

        public User(string id, string name, string timestamp)
        {
            Id = id;
            Name = name;
            Timestamp = timestamp;
        }
    }
}
