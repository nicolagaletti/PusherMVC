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

        private string _applicationKey;
        private string _applicaitonSecret;
        private string _applicationId;

        public PusherService(IPusher pusher, string applicationKey, string applicationSecret, string applicationId)
        {
            _pusher = pusher;
            _applicationKey = applicationKey;
            _applicaitonSecret = applicationSecret;
            _applicationId = applicationId;
        }
        
        public void UpdateStock(string productId)
        {
            throw new NotImplementedException();
        }
    }
}