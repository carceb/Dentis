using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class ClientController : Controller
    {
        private IClient _client;

        public ClientController(IClient client)
        {
            this._client = client;
        }

        public IActionResult SelectClient()
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                ClientViewModel clientViewModel = new ClientViewModel();
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                return View(clientViewModel);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult SelectClient(ClientViewModel clientViewModel)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                var model = _client.GetClientByIdentificationNumber(clientViewModel.IdentificationNumber).FirstOrDefault();

                if (model != null)
                {
                    return RedirectToAction("Index", "Budget", new { clientId = model.ClientId });
                }
                else
                {
                   return RedirectToAction("Add", new { idNumber = clientViewModel.IdentificationNumber});
                }

            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Edit(int clientId)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                ClientViewModel list = new ClientViewModel();
                var model = _client.GetClientById(clientId).FirstOrDefault();

                ViewBag.Gender = new SelectList(GetGenders());

                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Edit(ClientViewModel model)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                _client.SaveClient(model);
                return RedirectToAction("SelectClient");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Add(double? idNumber)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                ClientViewModel clientViewModel = new ClientViewModel();
                clientViewModel.IdentificationNumber = idNumber;

                ViewBag.Gender = new SelectList(GetGenders());
                return View(clientViewModel);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Add(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                int clientId = _client.SaveClient(clientViewModel);
                if (clientId > 0)
                {
                    return RedirectToAction("Index", "Budget", new { clientId = clientId });
                }
            }

            return RedirectToAction("Error", "Home");
        }

        private List<string> GetGenders()
        {
            List<string> gender = new List<string>();

            gender.Add("M");
            gender.Add("F");

            return gender;
        }
    }
}
