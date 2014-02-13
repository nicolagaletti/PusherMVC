﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client;

namespace PusherMvc.Web.App_Start
{
    public class DataDocumentStore
    {
        private static IDocumentStore _instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("The data document store hasn't been initialized.");
                }
                return _instance;
            }
        }

        public static IDocumentStore Initialize()
        {
            _instance = new EmbeddableDocumentStore 
            {
                ConnectionStringName = "RavenDB"
            };

            _instance.Conventions.IdentityPartsSeparator = "-";

            return _instance;
        }
    }
}