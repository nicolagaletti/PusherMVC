﻿using System.Security.Principal;
using PusherMvc.Data.Entities;
using PusherMvc.Web.Contracts;
using PusherRESTDotNet;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PresenceChannelData = PusherRESTDotNet.Authentication.PresenceChannelData;

namespace PusherMvc.Web.Services
{
    public class PusherService : IPusherService
    {
        private readonly IPusher _pusher;
        private readonly IPusherProvider _pusherProvider;

        public PusherService(IPusher pusher, IPusherProvider pusherProvider)
        {
            _pusher = pusher;
            _pusherProvider = pusherProvider;
        }
        
        public void UpdateStock(Product product)
        {
            if (product.StockLevel > 0)
            {
                _pusher.Trigger(String.Format("product-{0}", product.Id), "StockUpdated", product);
            }
        }

        public string Auth(string channelName, string socketId, string userName, object userInfo)
        {
            var channelData = new PresenceChannelData()
            {
                user_id = userName,
                user_info = userInfo
            };

            return _pusherProvider.Authenticate(channelName, socketId, channelData);
        }
    }
}