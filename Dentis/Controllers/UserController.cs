using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class UserController : Controller
    {
        private readonly ISecurity _security;
        private readonly IClinicConsulting _clinicConsulting;
        private readonly IClinic _clinic;

        public UserController(ISecurity security, IClinicConsulting clinicConsulting, IClinic clinic)
        {
            this._security = security;
            this._clinicConsulting = clinicConsulting;
            this._clinic = clinic;
        }
        public IActionResult Add()
        {
            int userId = 0;
            SecurityUserModel model = new SecurityUserModel();
            ViewBag.UserType = new SelectList(_security.GetUserTypes(), "SecurityUserTypeId", "SecurityUserTypeName");


            if (HttpContext.Session.GetInt32("SecurityUserId") != null)
            {
                userId = (int)HttpContext.Session.GetInt32("SecurityUserId");
            }

            if (!IsSuperUser())
            {
                ViewBag.Clinic = new SelectList(this._clinic.GetClinicByUserId(userId), "ClinicId", "ClinicName");
                ViewBag.ClinicConsulting = new SelectList(this._clinicConsulting.GetClinicConsultingUserByUserId(userId), "ClinicConsultingId", "ClinicConsultingName");
            }
            else
            {
                var firstClinc = this._clinic.GetClinics().FirstOrDefault();
                if (firstClinc != null)
                {
                    ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");
                    ViewBag.ClinicConsulting = new SelectList(this._clinicConsulting.GetClinicConsultingsByClinicId(firstClinc.ClinicId), "ClinicConsultingId", "ClinicConsultingName");
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(SecurityUserModel model)
        {
            if(ModelState.IsValid)
            {
                _security.AddOrEdit(model);

                return RedirectToAction("Add");
            }

            return RedirectToAction("Error", "Home");
        }

        public IActionResult Edit(int securityUserId)
        {
            int userId = 0;
            SecurityUserModel model = new SecurityUserModel();
            ViewBag.UserType = new SelectList(_security.GetUserTypes(), "SecurityUserTypeId", "SecurityUserTypeName");

            var list = _security.GetUserByUserId(securityUserId);

            foreach (var item in list)
            {
                model.SecurityUserId = item.SecurityUserId;
                model.SecurityUserName = item.SecurityUserName;
                model.SecurityUserStatus = item.SecurityUserStatus;
                model.SecurityUserTypeId = item.SecurityUserTypeId;
                model.UserPassword = item.UserPassword;
                model.UserLogin = item.UserLogin;
            }

            if (HttpContext.Session.GetInt32("SecurityUserId") != null)
            {
                userId = (int)HttpContext.Session.GetInt32("SecurityUserId");
            }

            if (!IsSuperUser())
            {
                ViewBag.Clinic = new SelectList(this._clinic.GetClinicByUserId(userId), "ClinicId", "ClinicName");
                ViewBag.ClinicConsulting = new SelectList(this._clinicConsulting.GetClinicConsultingUserByUserId(userId), "ClinicConsultingId", "ClinicConsultingName");
            }
            else
            {
                var firstClinc = this._clinic.GetClinics().FirstOrDefault();
                if (firstClinc != null)
                {
                    ViewBag.Clinic = new SelectList(this._clinic.GetClinics(), "ClinicId", "ClinicName");
                    ViewBag.ClinicConsulting = new SelectList(this._clinicConsulting.GetClinicConsultingsByClinicId(firstClinc.ClinicId), "ClinicConsultingId", "ClinicConsultingName");
                }
            }

            ViewBag.Status = new SelectList(GetUserStatus());    

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SecurityUserModel model)
        {
            if (ModelState.IsValid)
            {
                int clinicId = _security.AddOrEdit(model);

                return RedirectToAction("Index", "Setting");
            }

            return RedirectToAction("Error", "Home");
        }

        private List<string> GetUserStatus()
        {
            List<string> status = new List<string>();

            status.Add("0");
            status.Add("1");

            return status;
        }

        private bool IsSuperUser()
        {
            if (HttpContext.Session.GetString("SecurityUserTypeId") != null)
            {
                if (HttpContext.Session.GetInt32("SecurityUserTypeId") == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public JsonResult GetClinicConsultings(int clinicId)
        {
            List<SelectListItem> clinicConsultings = new List<SelectListItem>();

            foreach (var item in this._clinicConsulting.GetClinicConsultingsByClinicId(clinicId))
            {
                clinicConsultings.Add(new SelectListItem { Text = item.ClinicConsultingName, Value = item.ClinicConsultingId.ToString() });
            }

            return Json(new SelectList(clinicConsultings, "Value", "Text"));
        }
    }
}
