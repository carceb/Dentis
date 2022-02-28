using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class SettingController : Controller
    {
        private IClinic _clinic;
        private ISecurity _security;
        public SettingController(IClinic clinic, ISecurity security)
        {
            this._clinic = clinic;
            this._security = security;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectClinic()
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                ClinicViewModel clinicViewModel = new ClinicViewModel();
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");

                return View(clinicViewModel);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult SelectClinic(ClinicViewModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    if (model != null)
                    {
                        return RedirectToAction("Edit", "Clinic", new { clinicId = model.ClinicId });
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult SelectUser()
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                SecurityUserModel clinicViewModel = new SecurityUserModel();
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                ViewBag.User = new SelectList(this._security.GetUsers(), "SecurityUserId", "SecurityUserName");

                return View(clinicViewModel);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult SelectUser(SecurityUserModel model)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                if (model != null)
                {
                    return RedirectToAction("Edit", "User", new { securityUserId = model.SecurityUserId });
                }
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
