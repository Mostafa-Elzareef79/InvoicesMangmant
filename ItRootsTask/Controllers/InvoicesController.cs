using ItRootsTask.Controllers;
using ItRootsTask_Core.Features.InvoicesFeatures.Command;
using ItRootsTask_Core.Features.InvoicesFeatures.Query;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class InvoicesController:BaseApiController   
{
        private readonly ILogger<HomeController> _logger;

        public InvoicesController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Create()
        {
            TempData["CreatedMessage"] = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CmdAddEditInvoices invoice)
        {

            var Create = await Mediator.Send(invoice);
            if (Create.succeeded == true)
            {
                TempData["CreatedMessage"] = Create.message;
                return RedirectToAction("GridPage");
            }
            else
            {
                TempData["CreatedMessage"] = Create.message;
            }
            return View(invoice);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Create = await Mediator.Send( new CmdDeleteInvoice
            {
                Id = id
            });
          

            return RedirectToAction("GridPage");
        }
        public async Task<IActionResult> GridPage()
            
        {
            var GetAllData = await Mediator.Send(new GetAllInvoicesQuery());
            var invoicesList = GetAllData.data.ToList(); 

            return View("GridPage", invoicesList);

          
        }
    }
}
