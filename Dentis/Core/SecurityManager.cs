using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class SecurityManager : ISecurity
    {
        private IConfiguration Configuration;
        public SecurityManager(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public SecurityUserModel GetValidUser(string userLogin, string userPassword)
        {
            SecurityUserModel user = new SecurityUserModel();

            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("connectionString"));
            SqlCommand cmd = new SqlCommand("SELECT dbo.SecurityUser.SecurityUserId, dbo.SecurityUser.UserLogin, dbo.SecurityUser.UserPassword, dbo.SecurityUser.SecurityUserName, dbo.SecurityUser.SecurityUserStatus, " +
            "dbo.SecurityUser.SecurityUserTypeId, dbo.ClinicConsulting.ClinicConsultingId, dbo.ClinicConsulting.ClinicConsultingName, dbo.ClinicConsulting.ClinicConsultingPhone, dbo.Clinic.ClinicId, dbo.Clinic.ClinicRif, " +
            "dbo.Clinic.ClinicName FROM dbo.Clinic INNER JOIN  dbo.ClinicConsulting ON dbo.Clinic.ClinicId = dbo.ClinicConsulting.ClinicId INNER JOIN " +
            "dbo.SecurityClinicConsulting ON dbo.ClinicConsulting.ClinicConsultingId = dbo.SecurityClinicConsulting.ClinicConsultingId RIGHT OUTER JOIN " +
            "dbo.SecurityUser ON dbo.SecurityClinicConsulting.SecurityUserId = dbo.SecurityUser.SecurityUserId " +
            "WHERE UserLogin = '" + userLogin + "' AND UserPassword = '"+ userPassword + "' AND SecurityUserStatus = '1'", conn);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int clinicConsultingId = 0;
                    int clinicId = 0;

                    if (dr["ClinicConsultingId"].ToString() != "")
                    {
                        clinicConsultingId = (int)dr["ClinicConsultingId"];
                    }

                    if (dr["ClinicId"].ToString() != "")
                    {
                        clinicId = (int)dr["ClinicId"];
                    }

                    user.SecurityUserId = (int)dr["SecurityUserId"];
                    user.SecurityUserName = (string)dr["SecurityUserName"];
                    user.UserLogin = (string)dr["UserLogin"];
                    user.SecurityUserTypeId = (int)dr["SecurityUserTypeId"];
                    user.ClinicConsultingId = clinicConsultingId;
                    user.ClinicId = clinicId;
                    user.ClinicName = dr["ClinicName"].ToString();
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

            return user;
        }
    }
}
