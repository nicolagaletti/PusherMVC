using PusherMvc.Web.Contracts;
using PusherMvc.Web.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IDocumentSession _documentStore;

        public ProductRepository(IDocumentSession documentStore)
        {
            _documentStore = documentStore;
        }

        public void CreateProduct(ProductModel product)
        {
            _documentStore.Store(product);
        }

        public ProductModel[] ListProducts()
        {
            return _documentStore.Query<ProductModel>().OrderByDescending(pm => pm.Id).ToArray();
        }
    }
}