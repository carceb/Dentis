using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class Queues : IQueue
    {
        private readonly IConfiguration _configuration;
        public Queues(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<QueueStatusViewModel> GetQueueEstatus()
        {
            List<QueueStatusViewModel> queueEstatus = new List<QueueStatusViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM QueueEstatus ORDER BY QueueEstatusId", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clinicWebPage = string.Empty;
                        string clinicEmail = string.Empty;

                        queueEstatus.Add(new QueueStatusViewModel
                        {
                            QueueEstatusId = (int)dr["QueueEstatusId"],
                            QueueEstatusName = (string)dr["QueueEstatusName"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return queueEstatus.ToList();
        }

        public List<Queue> GetActiveQueue(int clinicConsultingId)
        {
            int day = (int)System.DateTime.Now.Day;
            int month = (int)System.DateTime.Now.Month;
            int year = (int)System.DateTime.Today.Year;

            List<Queue> queue = new List<Queue>();

            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("connectionString"));
            SqlCommand cmd = new SqlCommand("SELECT * FROM QueueDetail " +
            "WHERE InitYear = " + year + " AND InitDay = " + day + " " +
            "AND InitMonth = " + month + " AND ConsultingId = " + clinicConsultingId, conn);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    queue.Add(new Queue
                    {
                        QueueId = (int)dr["QueueId"],
                        InitDate = (DateTime)dr["InitDate"],
                        QueueStatusId = (int)dr["QueueStatusId"],
                        QueueStatusName = (string)dr["QueueEstatusName"],
                        PatientId = (int)dr["PatientId"],
                        PatientName = (string)dr["PatientName"],
                        PatientGender = (string)dr["PatientGender"],
                        PatientAge = (int)dr["PatientAge"],
                        AppointmentReasonId = (int)dr["AppointmentReasonId"],
                        AppointmentReasonName = (string)dr["AppointmentReasonName"],
                        ClinicConsultingId = (int)dr["ClinicConsultingId"],
                        ClinicConsultingName = (string)dr["ClinicConsultingName"]
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

            return queue;
        }

        public bool UpdateQueueStatus(int statusId, int patientId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Queue SET QueueStatusId = " + statusId + " WHERE PatientId = " + patientId, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}
