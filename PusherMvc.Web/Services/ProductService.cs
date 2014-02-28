using Microsoft.Ajax.Utilities;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusherMvc.Web.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public bool BuyProduct(string productId)
        {
            var bought = false;

            if (!productId.IsNullOrWhiteSpace())
            {
                var product = _productRepository.GetProductById(productId);

                if (product != null && product.StockLevel > 0)
                {
                    product.StockLevel--;
                    bought = true;
                }
            }
            return bought;
        }

        public Product GetProduct(string productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public Product[] ListProducts()
        {
            return _productRepository.ListProducts();
        }


        public void CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
        }
    }
}