using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class SecurityManager : ISecurity
    {
        private IConfiguration _configuration;
        public SecurityManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int AddOrEdit(SecurityUserModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    var userStatus = (model.SecurityUserId == 0 ? "1" : model.SecurityUserStatus);

                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UserAddOrEdit", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SecurityUserId", model.SecurityUserId);
                    cmd.Parameters.AddWithValue("UserLogin", model.UserLogin);
                    cmd.Parameters.AddWithValue("UserPassword", model.UserPassword);
                    cmd.Parameters.AddWithValue("SecurityUserStatus", userStatus);
                    cmd.Parameters.AddWithValue("SecurityUserName", model.SecurityUserName.ToUpper());
                    cmd.Parameters.AddWithValue("SecurityUserTypeId", model.SecurityUserTypeId);
                    cmd.Parameters.AddWithValue("ClinicConsultingId", model.ClinicConsultingId);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public SecurityUserModel GetValidUser(string userLogin, string userPassword)
        {
            SecurityUserModel user = new SecurityUserModel();

            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("connectionString"));
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

        public IList<SecurityUserModel> GetUsers()
        {
            List<SecurityUserModel> users = new List<SecurityUserModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM SecurityUser Order By SecurityUserName", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        users.Add(new SecurityUserModel
                        {
                            SecurityUserId = (int)dr["SecurityUserId"],
                            SecurityUserName = (string)dr["SecurityUserName"],
                            UserPassword = (string)dr["UserPassword"],
                            UserLogin = (string)dr["UserLogin"],
                            SecurityUserStatus = (string)dr["SecurityUserStatus"],
                            SecurityUserTypeId = (int)dr["SecurityUserTypeId"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return users.ToList();
        }

        public IList<SecurityUserModel> GetUserByUserId(int securityUserId)
        {
            List<SecurityUserModel> users = new List<SecurityUserModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT  dbo.SecurityUser.SecurityUserId, dbo.SecurityUser.UserLogin, dbo.SecurityUser.UserPassword, dbo.SecurityUser.SecurityUserName, dbo.SecurityUser.SecurityUserStatus, dbo.SecurityUser.SecurityUserTypeId, dbo.SecurityClinicConsulting.ClinicConsultingId,dbo.ClinicConsulting.ClinicId " +
                        " FROM dbo.SecurityUser INNER JOIN  dbo.SecurityClinicConsulting ON dbo.SecurityUser.SecurityUserId = dbo.SecurityClinicConsulting.SecurityUserId INNER JOIN dbo.ClinicConsulting ON dbo.SecurityClinicConsulting.ClinicConsultingId = dbo.ClinicConsulting.ClinicConsultingId " +
                        "WHERE dbo.SecurityUser.SecurityUserId = " + securityUserId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        users.Add(new SecurityUserModel
                        {
                            SecurityUserId = (int)dr["SecurityUserId"],
                            SecurityUserName = (string)dr["SecurityUserName"],
                            UserPassword = (string)dr["UserPassword"],
                            UserLogin = (string)dr["UserLogin"],
                            SecurityUserStatus = (string)dr["SecurityUserStatus"],
                            SecurityUserTypeId = (int)dr["SecurityUserTypeId"],
                            ClinicId = (int)dr["ClinicId"],
                            ClinicConsultingId = (int)dr["ClinicConsultingId"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return users.ToList();
        }

        public IList<SecurityUserModel> GetUserTypes()
        {
            List<SecurityUserModel> user = new List<SecurityUserModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM  dbo.SecurityUserType ORDER BY SecurityUserTypeId", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        user.Add(new SecurityUserModel
                        {
                            SecurityUserTypeId = (int)dr["SecurityUserTypeId"],
                            SecurityUserTypeName = (string)dr["SecurityUserTypeName"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return user.ToList();
        }
    }
}
