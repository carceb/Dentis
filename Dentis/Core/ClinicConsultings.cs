using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class ClinicConsultings : IClinicConsulting
    {
        private readonly IConfiguration _configuration;

        public ClinicConsultings(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IList<ClinicConsultingViewModel> GetClinicConsultings()
        {
            List<ClinicConsultingViewModel> clinicConsulting = new List<ClinicConsultingViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT dbo.ClinicConsulting.ClinicConsultingId, dbo.ClinicConsulting.ClinicId, dbo.ClinicConsulting.ClinicConsultingName, dbo.ClinicConsulting.ClinicConsultingPhone, dbo.Clinic.ClinicName, dbo.Clinic.ClinicStatus " +
                        "FROM  dbo.Clinic INNER JOIN dbo.ClinicConsulting ON dbo.Clinic.ClinicId = dbo.ClinicConsulting.ClinicId WHERE(dbo.Clinic.ClinicStatus = '1') ORDER BY dbo.ClinicConsulting.ClinicConsultingName", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clinicWebPage = string.Empty;
                        string clinicEmail = string.Empty;

                        clinicConsulting.Add(new ClinicConsultingViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicConsultingName = (string)dr["ClinicConsultingName"],
                            ClinicConsultingId = (int)dr["ClinicConsultingId"],
                            ClinicConsultingPhone = (string)dr["ClinicConsultingPhone"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return clinicConsulting.ToList();
        }

        public IList<ClinicConsultingViewModel> GetClinicConsultingsByClinicId(int clinicId)
        {
            List<ClinicConsultingViewModel> clinicConsulting = new List<ClinicConsultingViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT dbo.ClinicConsulting.ClinicConsultingId, dbo.ClinicConsulting.ClinicId, dbo.ClinicConsulting.ClinicConsultingName, dbo.ClinicConsulting.ClinicConsultingPhone, dbo.Clinic.ClinicName, dbo.Clinic.ClinicStatus " +
                        "FROM  dbo.Clinic INNER JOIN dbo.ClinicConsulting ON dbo.Clinic.ClinicId = dbo.ClinicConsulting.ClinicId WHERE dbo.Clinic.ClinicStatus = '1' AND dbo.ClinicConsulting.ClinicId = " + clinicId + " ORDER BY dbo.ClinicConsulting.ClinicConsultingName", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clinicWebPage = string.Empty;
                        string clinicEmail = string.Empty;

                        clinicConsulting.Add(new ClinicConsultingViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicConsultingName = (string)dr["ClinicConsultingName"],
                            ClinicConsultingId = (int)dr["ClinicConsultingId"],
                            ClinicConsultingPhone = (string)dr["ClinicConsultingPhone"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return clinicConsulting.ToList();
        }

        public IList<ClinicConsultingViewModel> GetClinicConsultingUserByUserId(int userId)
        {
            List<ClinicConsultingViewModel> clinic = new List<ClinicConsultingViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ClinicConsultingUser WHERE ClinicStatus = '1' AND SecurityUserId = " + userId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    { 
                        clinic.Add(new ClinicConsultingViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicConsultingId = (int)dr["ClinicConsultingId"],
                            ClinicConsultingName = (string)dr["ClinicConsultingName"],
                            ClinicConsultingPhone = (string)dr["ClinicConsultingPhone"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return clinic.ToList();
        }

        public IList<ClinicConsultingViewModel> GetClinicConsultingUserByClinicConsultingId(int clinicConsultingId)
        {
            List<ClinicConsultingViewModel> clinic = new List<ClinicConsultingViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ClinicConsultingUser WHERE ClinicStatus = '1' AND ClinicCOnsultingId = " + clinicConsultingId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clinic.Add(new ClinicConsultingViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicConsultingId = (int)dr["ClinicConsultingId"],
                            ClinicConsultingName = (string)dr["ClinicConsultingName"],
                            ClinicConsultingPhone = (string)dr["ClinicConsultingPhone"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return clinic.ToList();
        }

        public IList<ClinicConsultingViewModel> GetClinicConsultingByClinicConsultingId(int clinicConsultingId)
        {
            List<ClinicConsultingViewModel> clinic = new List<ClinicConsultingViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT dbo.ClinicConsulting.ClinicConsultingId, dbo.ClinicConsulting.ClinicId, dbo.ClinicConsulting.ClinicConsultingName, dbo.ClinicConsulting.ClinicConsultingPhone, dbo.Clinic.ClinicName, dbo.Clinic.ClinicStatus " +
                        "FROM  dbo.Clinic INNER JOIN dbo.ClinicConsulting ON dbo.Clinic.ClinicId = dbo.ClinicConsulting.ClinicId WHERE dbo.Clinic.ClinicStatus = '1' And dbo.ClinicConsulting.ClinicConsultingId = " + clinicConsultingId+ " ORDER BY dbo.ClinicConsulting.ClinicConsultingName", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clinic.Add(new ClinicConsultingViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicConsultingId = (int)dr["ClinicConsultingId"],
                            ClinicConsultingName = (string)dr["ClinicConsultingName"],
                            ClinicConsultingPhone = (string)dr["ClinicConsultingPhone"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return clinic.ToList();
        }

        public int SaveClinicConsulting(ClinicConsultingViewModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("ClinicConsultingAddOrEdit", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClinicConsultingId", model.ClinicConsultingId);
                    cmd.Parameters.AddWithValue("ClinicId", model.ClinicId);
                    cmd.Parameters.AddWithValue("ClinicConsultingName", model.ClinicConsultingName);
                    cmd.Parameters.AddWithValue("ClinicConsultingPhone", (!string.IsNullOrEmpty(model.ClinicConsultingPhone) ? model.ClinicConsultingPhone : "N/D"));

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
