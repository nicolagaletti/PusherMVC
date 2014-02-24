using System.Web.Mvc;
using AutoMapper;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Entities;
using PusherMvc.Web.App_Start;
using PusherMvc.Web.Mappings;
using PusherMvc.Web.Models;
using Raven.Client.Linq;
using System.Configuration;
using PusherServer;

namespace PusherMvc.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductRepository _productRepository;
        private IPusher _pusher;

        public StoreController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var result = _productRepository.ListProducts();

            var viewModelResults = Mapper.Map<Product[], ProductListItemViewModel[]>(result);

            return View(viewModelResults);
        }

        //
        // GET: /Store/ProductDetails/products-5

        public ActionResult ProductDetails(string id)
        {
            var result = _productRepository.GetProductById(id);

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
                
                _productRepository.CreateProduct(dataItem);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
