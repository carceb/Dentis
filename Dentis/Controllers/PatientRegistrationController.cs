using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class PatientRegistrationController : Controller
    {
        private IPatient _patient;
        private IAppointmentReason _appointmentReason;

        public PatientRegistrationController(IPatient patient, IAppointmentReason appointmentReason)
        {
            this._patient = patient;
            this._appointmentReason = appointmentReason;
        }

        public IActionResult Add()
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    PatientViewModel patientViewModel = new PatientViewModel();

                    ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                    ViewBag.PatientGender = new SelectList(GetGenders());
                    ViewBag.PatientAges = new SelectList(GetAges());
                    ViewBag.AppointmentReason = new SelectList(this._appointmentReason.GetAppointmentReasons(), "AppointmentReasonId", "AppointmentReasonName");

                    return View(patientViewModel);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult Add(PatientViewModel patientViewModel)
        {
            try
            {
                if (HttpContext.Session.GetString("SecurityUserId") != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (HttpContext.Session.GetInt32("ClinicConsultingId") != null)
                        {
                            patientViewModel.ClinicConsultingId = (int)HttpContext.Session.GetInt32("ClinicConsultingId");
                        }

                        if (_patient.AddOrEdit(patientViewModel))
                        {
                            return RedirectToAction(nameof(Add));
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

        private List<string> GetGenders()
        {
            List<string> gender = new List<string>();

            gender.Add("M");
            gender.Add("F");

            return gender;
        }

        private List<int> GetAges()
        {
            List<int> ages = new List<int>();

            for (int i = 0; i < 99; i++)
            {
                ages.Add(i);
            }

            return ages;
        }
    }
}
