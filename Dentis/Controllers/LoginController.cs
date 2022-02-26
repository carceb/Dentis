using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class LoginController : Controller
    {
        private ISecurity security;
        public LoginController(ISecurity _security)
        {
            this.security = _security;
        }
        public IActionResult Index()
        {
            HttpContext.Session.Remove("SecurityUserId");
            HttpContext.Session.Remove("SecurityUserTypeId");
            HttpContext.Session.Remove("SecurityUserName");
            HttpContext.Session.Remove("ClinicConsultingId");
            HttpContext.Session.Remove("ClinicId");
            HttpContext.Session.Remove("ClinicName");
            HttpContext.Session.Remove("ClinicConsultingName");

            SecurityUserModel model = new SecurityUserModel();
            return View(model);
        }

        public IActionResult AccesLogin(SecurityUserModel model)
        {
            try
            {
                var result = security.GetValidUser(model.UserLogin, model.UserPassword);

                if (result != null && result.SecurityUserId != 0)
                {
                    HttpContext.Session.SetInt32("SecurityUserId", result.SecurityUserId);
                    HttpContext.Session.SetInt32("SecurityUserTypeId", result.SecurityUserTypeId);
                    HttpContext.Session.SetString("SecurityUserName", result.SecurityUserName);

                    return RedirectToAction("Index", "ConsultingSelection");
                }

                ViewBag.InvalidUser = "true";

                return View("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home" , new {errorMessage = e.Message.ToString() });
            }
        }
    }
}
