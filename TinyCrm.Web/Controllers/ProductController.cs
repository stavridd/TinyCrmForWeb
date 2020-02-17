using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class ProductController : Controller
    {

        private TinyCrmDbContext Context { get; set; }

        private Core.Services.IProductService products_;
        public ProductController(TinyCrmDbContext context,
            Core.Services.IProductService product)
        {
             
             Context = context;
             products_ = product;
        }

        public IActionResult Index()
        {
            var productList = Context
                .Set<Product>()
                .Take(100)
                .ToList();
            var customerList = Context
                .Set<Customer>()
                .Take(100)
                .ToList();

            var model = new Models.CustomersProductsViewModel()
            {
                Customers = customerList,
                Products = productList
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Models.CreateProductViewModel model)
        {
            var result = products_.AddProduct(
                model?.CreateOptions);

            if (result == false) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(
            Models.SearchProductViewModel model)
        {
            var result = products_.SearchProduct(
                model?.SearchOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }
    }
}