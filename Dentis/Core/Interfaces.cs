using Dentis.Models;

namespace Dentis.Core
{
    public class Interfaces
    {
        public interface IPatient
        {
            public List<Patient> GetPatients();
            public bool SavePatient(PatientViewModel model);
        }

        public interface IQueue
        {
            public List<Queue> GetActiveQueue(int clinicConsultingId);
            public IList<QueueStatusViewModel> GetQueueEstatus();
        }

        public interface ISecurity
        {
            public SecurityUserModel GetValidUser(string userLogin, string userPassword);
        }

        public interface IAppointmentReason
        {
            public IList<AppointmentReason> GetAppointmentReasons();
        }
        public interface IClinic
        {
            public int SaveClinic(ClinicViewModel model);
            public IList<ClinicViewModel> GetClinics();
            public IList<ClinicViewModel> GetClinicByUserId(int userId);
            public IList<ClinicViewModel> GetClinicById(int clinicId);
        }
        public interface IClinicConsulting
        {
            public int SaveClinicConsulting(ClinicConsultingViewModel model);
            public IList<ClinicConsultingViewModel> GetClinicConsultingUserByUserId(int userId);
            public IList<ClinicConsultingViewModel> GetClinicConsultings();
            public IList<ClinicConsultingViewModel> GetClinicConsultingsByClinicId(int clinicId);
            public IList<ClinicConsultingViewModel> GetClinicConsultingUserByClinicConsultingId(int clinicConsulting);
            public IList<ClinicConsultingViewModel> GetClinicConsultingByClinicConsultingId(int clinicConsulting);            
        }
    }
}
