using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using Raven.Client;

namespace PusherMvc.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDocumentSession _documentSession;
        
        public ProductRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public void CreateProduct(Product product)
        {
            _documentSession.Store(product);
        }

        public Product[] ListProducts()
        {
            return _documentSession.Query<Product>().ToArray();
        }

        public Product GetProductById(string Id)
        {
            return _documentSession.Load<Product>(Id);
        }
    }
}
