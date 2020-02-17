using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        private TinyCrmDbContext Context { get; set; }
        private Core.Services.ICustomerService customers_;

        public CustomerController(TinyCrmDbContext context,
                    Core.Services.ICustomerService customers)
        {

            Context = context;
            customers_ = customers;
        }

        public IActionResult Index()
        {
            var customerList = Context
                .Set<Customer>()
                .Take(100)
                .ToList();

            return View(customerList);
        }

        public async Task<IActionResult> List()
        {
            var customerList = await Context
                .Set<Customer>()
                .Select(c => new { c.Email, c.VatNumber })
                .Take(100)
                .ToListAsync();

            return Json(customerList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Models.CreateCustomerViewModel model)
        {
            var result = customers_.CreateCustomer(
                model?.CreateOptions);

            if (result == null) {
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
            Models.SearchCustomerViewModel model)
        {
            var result = customers_.SearchCustomers(
                model?.SearchOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }
    }
}