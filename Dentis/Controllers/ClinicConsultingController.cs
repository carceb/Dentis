using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class ClinicConsultingController : Controller
    {
        private readonly IClinicConsulting _clinicConsulting;
        private readonly IClinic _clinic;
        public ClinicConsultingController(IClinicConsulting clinicConsulting, IClinic clinic)
        {
            this._clinicConsulting = clinicConsulting;
            this._clinic = clinic;
        }

        public IActionResult Add(int clinicIdSaved)
        {
            ClinicConsultingViewModel model = new ClinicConsultingViewModel();
            model.ClinicId = clinicIdSaved;

            ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");

            return View(model);
        }

        [HttpPost()]
        public IActionResult Add(ClinicConsultingViewModel model)
        {
            if (ModelState.IsValid)
            {                    
                int clinicConsultingId = _clinicConsulting.SaveClinicConsulting(model);
                ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");

                if (clinicConsultingId != 0)
                {
                    if (!IsSuperUser())
                    {
                        return RedirectToAction("Add", new { clinicIdSaved = model.ClinicId });
                    }

                    return RedirectToAction("AddNewConsulting");
                }
            }

            return RedirectToAction("Error", "Home");
        }

        public IActionResult AddNewConsulting()
        {
            ClinicConsultingViewModel model = new ClinicConsultingViewModel();

            ViewBag.ClinicName = "Super Admin";
            ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");

            return View(model);
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
    }
}
