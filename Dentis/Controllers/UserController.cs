using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class UserController : Controller
    {
        private ISecurity _security;
        public UserController(ISecurity security)
        {
            this._security = security;
        }
        public IActionResult Add()
        {
            SecurityUserModel model = new SecurityUserModel();
            ViewBag.UserType = new SelectList(_security.GetUserTypes(), "SecurityUserTypeId", "SecurityUserTypeName");

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
    }
}
