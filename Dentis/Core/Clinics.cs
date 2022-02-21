using static Dentis.Core.Interfaces;
using Dentis.Models;
using System.Data.SqlClient;

namespace Dentis.Core
{
    public class Clinics : IClinic
    {
        private readonly IConfiguration _configuration;
        public Clinics(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IList<ClinicViewModel> GetClinics()
        {
            List<ClinicViewModel> clinic = new List<ClinicViewModel>();
            
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Clinic WHERE ClinicStatus = '1' Order By ClinicName", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clinicWebPage = string.Empty;
                        string clinicEmail = string.Empty;

                        if (dr["WebPage"].ToString() != "")
                        {
                            clinicWebPage = (string)dr["WebPage"];
                        }

                        if (dr["ClinicEmail"].ToString() != "")
                        {
                            clinicEmail = (string)dr["ClinicEmail"];
                        }

                        clinic.Add(new ClinicViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicRif = (string)dr["ClinicRif"],
                            ClinicEmail = clinicEmail,
                            ClinicStatus = (string)dr["ClinicStatus"],
                            ClinicAddress = (string)dr["ClinicAddress"],
                            ClinicPhoneNumber = (string)dr["ClinicPhoneNumber"],
                            WebPage = clinicWebPage
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

        public IList<ClinicViewModel> GetClinicById(int clinicId)
        {
            List<ClinicViewModel> clinic = new List<ClinicViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Clinic WHERE ClinicStatus = '1' And ClinicId = " + clinicId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clinicWebPage = string.Empty;
                        string clinicEmail = string.Empty;

                        if (dr["WebPage"].ToString() != "")
                        {
                            clinicWebPage = (string)dr["WebPage"];
                        }

                        if (dr["ClinicEmail"].ToString() != "")
                        {
                            clinicEmail = (string)dr["ClinicEmail"];
                        }

                        clinic.Add(new ClinicViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"],
                            ClinicRif = (string)dr["ClinicRif"],
                            ClinicEmail = clinicEmail,
                            ClinicStatus = (string)dr["ClinicStatus"],
                            ClinicAddress = (string)dr["ClinicAddress"],
                            ClinicPhoneNumber = (string)dr["ClinicPhoneNumber"],
                            WebPage = clinicWebPage
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

        public IList<ClinicViewModel> GetClinicByUserId(int userId)
        {
            List<ClinicViewModel> clinic = new List<ClinicViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ClinicUser WHERE ClinicStatus = '1' AND SecurityUserId = " + userId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clinic.Add(new ClinicViewModel
                        {
                            ClinicId = (int)dr["ClinicId"],
                            ClinicName = (string)dr["ClinicName"]
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
        public int AddOrEdit(ClinicViewModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("ClinicAddOrEdit", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClinicId", model.ClinicId);
                    cmd.Parameters.AddWithValue("ClinicName", model.ClinicName);
                    cmd.Parameters.AddWithValue("ClinicRif", model.ClinicRif);
                    cmd.Parameters.AddWithValue("ClinicAddress", model.ClinicAddress);
                    cmd.Parameters.AddWithValue("ClinicPhoneNumber", model.ClinicPhoneNumber);
                    cmd.Parameters.AddWithValue("WebPage", (!string.IsNullOrEmpty(model.WebPage) ? model.WebPage : "N/D"));
                    cmd.Parameters.AddWithValue("ClinicStatus", (!string.IsNullOrEmpty(model.ClinicStatus) ? model.ClinicStatus : "1"));

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
