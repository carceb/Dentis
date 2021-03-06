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
            return RedirectToAction("AddNewConsulting");
        }

        [HttpPost()]
        public IActionResult Add(ClinicConsultingViewModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    if (ModelState.IsValid)
                    {
                        int clinicConsultingId = _clinicConsulting.AddOrEdit(model);
                        ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");

                        if (clinicConsultingId != 0)
                        {
                            if (!Utils.Utils.IsSuperUser((int)HttpContext.Session.GetInt32("SecurityUserTypeId")))
                            {
                                return RedirectToAction("Add", new { clinicIdSaved = model.ClinicId });
                            }

                            return RedirectToAction("AddNewConsulting");
                        }
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }            
        }

        public IActionResult AddNewConsulting()
        {
            try
            {
                ClinicConsultingViewModel model = new ClinicConsultingViewModel();

                ViewBag.ClinicName = "Super Admin";
                ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() }); ;
            }
        }
    }
}
