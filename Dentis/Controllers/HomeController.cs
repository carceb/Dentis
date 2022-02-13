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
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                ViewBag.UserName = UserName();
                ViewBag.ClinicName = (string)HttpContext.Session.GetString("ClinicName");
                ViewBag.ClinicConsultingName = (string)HttpContext.Session.GetString("ClinicConsultingName");
                return View(_patient.GetPatients());
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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