using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class Patients : IPatient
    {
        private IConfiguration _configuration;
        public Patients(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public bool AddOrEdit(PatientViewModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("PatientAddOrEdit", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("PatientId", model.PatientId);
                    cmd.Parameters.AddWithValue("PatientName", model.PatientName.ToUpper());
                    cmd.Parameters.AddWithValue("PatientAge", model.PatientAge);
                    cmd.Parameters.AddWithValue("PatientGender", model.PatientGender);
                    cmd.Parameters.AddWithValue("AppointmentReasonId", model.AppointmentReasonId);
                    cmd.Parameters.AddWithValue("ClinicConsultingId", model.ClinicConsultingId);
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Patient> GetPatients() 
        {
            List<Patient> patient = new List<Patient>();

            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("connectionString"));
            SqlCommand cmd = new SqlCommand("SELECT  dbo.Patient.PatientId, dbo.Patient.PatientName, dbo.Patient.PatientAge, dbo.Patient.PatientGender, dbo.Patient.AppointmentReasonId, dbo.AppointmentReason.AppointmentReasonName " +
                "FROM dbo.Patient INNER JOIN  dbo.AppointmentReason ON dbo.Patient.AppointmentReasonId = dbo.AppointmentReason.AppointmentReasonId", conn);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    patient.Add(new Patient
                    {
                        PatientId = (int)dr["PatientId"],
                        PatientName = (string)dr["PatientName"],
                        PatientGender = (string)dr["PatientGender"],
                        PatientAge = (int)dr["PatientAge"],
                        AppointmentReasonId = (int)dr["AppointmentReasonId"],
                        AppointmentReasonName = (string)dr["AppointmentReasonName"]
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }          
            
            return patient;
        }
    }
}
