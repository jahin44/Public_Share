using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.web.Areas.Admin.Models;
using TicketBookingSystem.web.Models;

namespace TicketBookingSystem.web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminAccess")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new CustomerListModel();
            return View(model);
        }

        public JsonResult GetCustomerData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new CustomerListModel();
            var data = model.GetCustomers(dataTablesModel);
            return Json(data);
        }

        public IActionResult Buy()
        {
            var model = new BuyTicketsModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Buy(BuyTicketsModel model)
        {
            if (ModelState.IsValid)
            {
                model.BuyTicket();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var model = new CreateCustomerModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateCustomer();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Customer");
                    _logger.LogError(ex, "Create Customer Failed");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new EditCustomerModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new CustomerListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
