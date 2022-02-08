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
        public IActionResult Add()
        {
            ClientViewModel patientViewModel = new ClientViewModel();

            ViewBag.Gender = new SelectList(GetGenders());
            return View(patientViewModel);
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
