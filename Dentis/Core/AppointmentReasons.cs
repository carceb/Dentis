using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class AppointmentReasons : IAppointmentReason
    {
        private IConfiguration _configuration;
        public AppointmentReasons(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<AppointmentReason> GetAppointmentReasons()
        {
            List<AppointmentReason> appointmentReasons = new List<AppointmentReason>();

            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("connectionString"));
            SqlCommand cmd = new SqlCommand("SELECT * FROM AppointmentReason" , conn);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    appointmentReasons.Add(new AppointmentReason
                    { 
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

            return appointmentReasons.ToList();
        }
    }
}
