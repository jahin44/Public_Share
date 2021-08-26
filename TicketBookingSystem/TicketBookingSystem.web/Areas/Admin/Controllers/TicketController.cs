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
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new TicketListModel();
            return View(model);
        }

        public JsonResult GetTicketData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new TicketListModel();
            var data = model.GetTickets(dataTablesModel);
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
            var model = new CreateTicketModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateTicket();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create ticket");
                    _logger.LogError(ex, "Create Ticket Failed");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new EditTicketModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditTicketModel model)
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
            var model = new TicketListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
