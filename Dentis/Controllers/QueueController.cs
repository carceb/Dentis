using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class QueueController : Controller
    {
        private readonly IQueue _queue;
        private readonly IClinicConsulting _clinicConsulting;

        public QueueController(IQueue queue, IClinicConsulting clinicConsulting)
        {
            this._queue = queue;
            this._clinicConsulting = clinicConsulting; 
        }
        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("ClinicConsultingName") != null)
                {
                    var hostName = (Request.Host.Value.Contains("localhost:80") ? "localhost/Dentis" : Request.Host.Value);
                    ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                    ViewBag.ShareLink = $"{Request.Scheme}://{hostName}{Request.Path.Value}/CheckQueueFromExternal?clinicConsultingId={(int)HttpContext.Session.GetInt32("ClinicConsultingId")}";
                    ViewBag.QueueStatus = new SelectList(this._queue.GetQueueEstatus(), "QueueEstatusId", "QueueEstatusName");
                    return View(_queue.GetActiveQueue((int)HttpContext.Session.GetInt32("ClinicConsultingId")));
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }

        }
        public IActionResult CheckQueueFromExternal(int clinicConsultingId)
        {
            try
            {
                var clinicConsulting = _clinicConsulting.GetClinicConsultingByClinicConsultingId(clinicConsultingId);

                ViewBag.ClinicConsultingId = clinicConsultingId;

                if (clinicConsulting.Any())
                {
                    ViewBag.ClinicConsultingName = clinicConsulting.FirstOrDefault().ClinicConsultingName;
                }
                else
                {
                    ViewBag.ClinicConsultingName = "N/D";
                }

                return View(_queue.GetActiveQueue(clinicConsultingId));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult UpdateStatus(int patiendId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_queue.UpdateQueueStatus(2, patiendId))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }
    }
}
