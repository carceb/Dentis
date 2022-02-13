using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class ConsultingSelectionController : Controller
    {
        private readonly IClinicConsulting _clinicConsulting;
        private readonly IClinic _clinic;

        public ConsultingSelectionController(IClinicConsulting clinicConsulting, IClinic clinic)
        {
            this._clinicConsulting = clinicConsulting;
            this._clinic = clinic;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                int userId = 0;

                ClinicConsultingViewModel model = new ClinicConsultingViewModel();

                if (HttpContext.Session.GetInt32("SecurityUserId") != null)
                {
                    userId = (int)HttpContext.Session.GetInt32("SecurityUserId");
                }

                if (!IsSuperUser())
                {
                    ViewBag.Clinic = new SelectList(this._clinic.GetClinicByUserId(userId), "ClinicId", "ClinicName");
                    ViewBag.ClinicConsulting = new SelectList(this._clinicConsulting.GetClinicConsultingUserByUserId(userId), "ClinicConsultingId", "ClinicConsultingName");
                }
                else
                {
                    var firstClinc = this._clinic.GetClinics().FirstOrDefault();
                    if (firstClinc != null)
                    {
                        ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");
                        ViewBag.ClinicConsulting = new SelectList(this._clinicConsulting.GetClinicConsultingsByClinicId(firstClinc.ClinicId), "ClinicConsultingId", "ClinicConsultingName");
                    }
                }

                return View();
            }
            else 
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Index(ClinicConsultingViewModel model)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                HttpContext.Session.SetInt32("ClinicConsultingId", model.ClinicConsultingId);
                HttpContext.Session.SetInt32("ClinicId", model.ClinicId);
                HttpContext.Session.SetString("ClinicName", _clinic.GetClinicById(model.ClinicId).FirstOrDefault().ClinicName);
                HttpContext.Session.SetString("ClinicConsultingName", _clinicConsulting.GetClinicConsultingByClinicConsultingId(model.ClinicConsultingId).FirstOrDefault().ClinicConsultingName);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        private bool IsSuperUser()
        {
            if (HttpContext.Session.GetString("SecurityUserTypeId") != null)
            {
                if (HttpContext.Session.GetInt32("SecurityUserTypeId") == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public JsonResult GetClinicConsultings(int clinicId)
        {
            List<SelectListItem> clinicConsultings = new List<SelectListItem>();

            foreach (var item in this._clinicConsulting.GetClinicConsultingsByClinicId(clinicId))
            {
                clinicConsultings.Add(new SelectListItem { Text = item.ClinicConsultingName, Value = item.ClinicConsultingId.ToString() });
            }

            return Json(new SelectList(clinicConsultings, "Value", "Text"));
        }
    }
}
