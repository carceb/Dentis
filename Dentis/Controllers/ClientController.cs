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

        public IActionResult SelectClient(string budgetType = "PrintBudget")
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    if (budgetType == "PrintBudget")
                    {
                        ClientViewModel clientViewModel = new ClientViewModel();
                        ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                        return View(clientViewModel);
                    }
                    else
                    {
                        ClientViewModel clientViewModel = new ClientViewModel();
                        ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                        return View(clientViewModel);
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult SelectClient(ClientViewModel clientViewModel)
        {
            try
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
                        return RedirectToAction("Add", new { idNumber = clientViewModel.IdentificationNumber });
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult Edit(int clientId)
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                    ClientViewModel list = new ClientViewModel();
                    var model = _client.GetClientById(clientId).FirstOrDefault();

                    ViewBag.Gender = new SelectList(GetGenders());

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() }); ;
            }
        }

        [HttpPost]
        public IActionResult Edit(ClientViewModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                    _client.AddOrEdit(model);
                    return RedirectToAction("SelectClient");
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() }); ; ;
            }
        }

        public IActionResult Add(double? idNumber)
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                    ClientViewModel clientViewModel = new ClientViewModel();
                    clientViewModel.IdentificationNumber = idNumber;

                    ViewBag.Gender = new SelectList(GetGenders());
                    return View(clientViewModel);
                }

                return RedirectToAction("Index", "Login");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }

        }

        [HttpPost]
        public IActionResult Add(ClientViewModel clientViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int clientId = _client.AddOrEdit(clientViewModel);
                    if (clientId > 0)
                    {
                        return RedirectToAction("Index", "Budget", new { clientId = clientId });
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }

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
