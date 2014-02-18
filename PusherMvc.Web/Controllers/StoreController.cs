using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using PusherMvc.Web.Contracts;
using PusherMvc.Web.Models;

namespace PusherMvc.Web.Controllers
{
    public class StoreController : Controller
    {
        //private IDocumentSession _documentStore;
        private IProductRepository _productRepository;

        //public StoreController(IDocumentSession documentStore)
        //{
        //    _documentStore = documentStore;
        //}

        public StoreController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        //
        // GET: /Store/

        public ActionResult Index()
        {
            //var result = _documentStore.Query<ProductModel>().OrderByDescending(pm => pm.Id).ToArray();

            var result = _productRepository.ListProducts();

            return View(result);
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult CreateProduct(ProductModel product)
        {
            try
            {
//                _documentStore.Store(product);
                _productRepository.CreateProduct(product);

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
