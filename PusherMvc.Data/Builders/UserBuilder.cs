using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PusherMvc.Data.Builders
{
    public class UserBuilder : IUserBuilder
    {
        private string _id;
        private string _name;
        private string _timestamp;
        
        public User Build()
        {
            return new User(_id, _name, _timestamp);
        }

        public UserBuilder WithId(string name, string sessionId)
        {
            var id = "";
            
            using (HashAlgorithm md5 = new MD5CryptoServiceProvider())
            {
                id = String.Format("{0}_{1}_{2}", DateTime.UtcNow.Ticks, name, sessionId);

                var byteId = Encoding.ASCII.GetBytes(id);

                var encryptedByteId = md5.ComputeHash(byteId);

                id = Encoding.ASCII.GetString(encryptedByteId);
            }

            _id = id;

            return this;
        }

        public UserBuilder WithName(string inputName, int count)
        {
            string name = inputName;
            
            if(String.IsNullOrEmpty(name))
            {
                name = String.Format("Guest {0}" + count);
            }

            _name = name;

            return this;
        }

        public UserBuilder WithTimestamp()
        {
            var objUTC = DateTime.Now.ToUniversalTime();
            _timestamp = ((objUTC.Ticks - 621355968000000000) / 10000).ToString();

            return this;
        }

        public static implicit operator User(UserBuilder instance)
        {
            return instance.Build();
        }
    }
}
