using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class HomeController : Controller
    {
        private IPatient _patient;

        public HomeController(IPatient patient)
        {
            _patient = patient;
        }

        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    ViewBag.UserName = UserName();
                    ViewBag.IsSuperUser = Utils.Utils.IsSuperUser((int)HttpContext.Session.GetInt32("SecurityUserTypeId"));
                    ViewBag.ClinicName = (string)HttpContext.Session.GetString("ClinicName");
                    ViewBag.ClinicConsultingName = (string)HttpContext.Session.GetString("ClinicConsultingName");
                    return View(_patient.GetPatients());
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }
        private string UserName()
        {
            if (HttpContext.Session.GetString("SecurityUserName") != null)
            {
                return HttpContext.Session.GetString("SecurityUserName").ToString();
            }

            return string.Empty;
        }
    }
}