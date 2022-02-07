using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class QueueController : Controller
    {
        private readonly IQueue _queue;

        public QueueController(IQueue  queue)
        {
            this._queue = queue;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ClinicConsultingName") != null)
            {
                ViewBag.QueueStatus = new SelectList(this._queue.GetQueueEstatus(), "QueueEstatusId", "QueueEstatusName");
                return View(_queue.GetActiveQueue((int)HttpContext.Session.GetInt32("ClinicConsultingId")));
            }
          
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult UpdateStatus(QueueStatusViewModel queueStatusViewModel)
        {
            if (ModelState.IsValid)
            {

                if (_patient.SavePatient(patientViewModel))
                {
                    return RedirectToAction(nameof(Add));
                }
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
