using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinic _clinic;
        public ClinicController(IClinic clinic)
        {
            this._clinic = clinic;
        }
        public IActionResult Add()
        {
            ClinicViewModel model = new ClinicViewModel();

            int? securityUserTypeId = HttpContext.Session.GetInt32("SecurityUserTypeId");
            int? clinicId = HttpContext.Session.GetInt32("ClinicId");
            if (securityUserTypeId == 1)
            {
               return View(model);
            }

            return RedirectToAction("Add", "ClinicConsulting", new { clinicIdSaved = clinicId });
        }

        [HttpPost]
        public IActionResult Add(ClinicViewModel model)
        {
            if (ModelState.IsValid)
            {
                int clinicId = _clinic.SaveClinic(model);
                if (clinicId != 0)
                {
                    return RedirectToAction("Add", "ClinicConsulting", new { clinicIdSaved = clinicId });
                }
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
