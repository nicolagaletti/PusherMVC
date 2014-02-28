using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.App_Start;
using PusherMvc.Web.Mappings;
using PusherMvc.Web.Models;
using Raven.Client.Linq;
using System.Configuration;
using PusherServer;
using PusherMvc.Web.Contracts;
using System;

namespace PusherMvc.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private IPusherService _pusherService;

        public StoreController(IProductService productRepository, IPusherService pusherService)
        {
            _productService = productRepository;
            _pusherService = pusherService;
        }
        
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var result = _productService.ListProducts();

            var viewModelResults = Mapper.Map<Product[], ProductListItemViewModel[]>(result);

            return View(viewModelResults);
        }

        //
        // GET: /Store/ProductDetails/products-5

        [HttpGet]
        public ActionResult ProductDetails(string id)
        {
            var result = _productService.GetProduct(id);

            var viewModelResult = Mapper.Map<Product, AddProductViewModel>(result);
            
            return View(viewModelResult);
        }

        //
        // GET: /Store/Create

        public ActionResult CreateProduct()
        {
            return View();
        }

        //
        // POST: /Store/Create

        [HttpPost]
        public ActionResult CreateProduct(AddProductViewModel addProductViewModel)
        {
            try
            {
                Product dataItem = Mapper.Map<AddProductViewModel, Product>(addProductViewModel);
                
                _productService.CreateProduct(dataItem);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateStock(string productId)
        {
            try
            {
                var bought = _productService.BuyProduct(productId);

                if (bought)
                {
                    var product = _productService.GetProduct(productId);
                    
                    _pusherService.UpdateStock(product);
                }

                return RedirectToAction("ProductDetails", new {id = productId});
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Store/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Store/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Store/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Store/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
