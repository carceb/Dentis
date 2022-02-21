using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                int clinicId = _clinic.AddOrEdit(model);
                if (clinicId != 0)
                {
                    return RedirectToAction("Add", "ClinicConsulting", new { clinicIdSaved = clinicId });
                }
            }

            return RedirectToAction("Error", "Home");
        }
        public IActionResult Edit(int clinicId)
        {
            ClinicViewModel model = new ClinicViewModel();
            var list = _clinic.GetClinicById(clinicId);

            foreach (var item in list)
            {
                model.ClinicId = item.ClinicId;
                model.ClinicName = item.ClinicName;
                model.ClinicRif = item.ClinicRif;
                model.ClinicAddress = item.ClinicAddress;
                model.ClinicEmail = item.ClinicEmail;
                model.ClinicPhoneNumber = item.ClinicPhoneNumber;
                model.WebPage = item.WebPage;
                model.ClinicStatus = item.ClinicStatus;
            }

            ViewBag.Status = new SelectList(GetClinicStatus());

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ClinicViewModel model)
        {
            if (ModelState.IsValid)
            {
                int clinicId = _clinic.AddOrEdit(model);

                return RedirectToAction("Index", "Setting");
            }

            return RedirectToAction("Error", "Home");
        }

        private List<string> GetClinicStatus()
        {
            List<string> status = new List<string>();

            status.Add("0");
            status.Add("1");

            return status;
        }
    }
}
