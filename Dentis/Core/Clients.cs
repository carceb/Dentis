using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class Clients: IClient
    {
        private IConfiguration _configuration;
        public Clients(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public int AddOrEdit(ClientViewModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    var cultureInfo = new System.Globalization.CultureInfo("de-DE");
                    string dateString = model.BirthDate;
                    var dateTimeBirthDate = DateTime.Parse(dateString, cultureInfo);

                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("ClientAddOrEdit", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClientId", model.ClientId);
                    cmd.Parameters.AddWithValue("IdentificationNumber", model.IdentificationNumber);
                    cmd.Parameters.AddWithValue("ClientName", model.ClientName.ToUpper());
                    cmd.Parameters.AddWithValue("Gender", model.Gender);
                    cmd.Parameters.AddWithValue("BirthDate", dateTimeBirthDate);
                    cmd.Parameters.AddWithValue("ClientAddress", (!string.IsNullOrEmpty(model.ClientAddress) ? model.ClientAddress.ToUpper() : "N/D"));
                    cmd.Parameters.AddWithValue("ClientCellPhone",  (!string.IsNullOrEmpty(model.ClientCellPhone) ? model.ClientCellPhone : "N/D"));
                    cmd.Parameters.AddWithValue("ClientEmail", (!string.IsNullOrEmpty(model.ClientEmail) ? model.ClientEmail : "N/D"));

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
                return 0;
            }
        }

        public IList<ClientViewModel> GetClientById(int clientId)
        {
            List<ClientViewModel> client = new List<ClientViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CLient WHERE ClientId = " + clientId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clientAddress = string.Empty;
                        string clientCellPhone = string.Empty;
                        string clientEmail = string.Empty;

                        DateTime dateBirthDate = (DateTime)dr["BirthDate"];
                        string stringBirthDate = dateBirthDate.ToString("dd/MM/yyyy");

                        if (dr["ClientAddress"].ToString() != "")
                        {
                            clientAddress = (string)dr["ClientAddress"];
                        }

                        if (dr["ClientCellPhone"].ToString() != "")
                        {
                            clientCellPhone = (string)dr["ClientCellPhone"];
                        }

                        if (dr["ClientEmail"].ToString() != "")
                        {
                            clientEmail = (string)dr["ClientEmail"];
                        }

                        client.Add(new ClientViewModel
                        {
                            ClientId = (int)dr["ClientId"],
                            IdentificationNumber = (double)dr["IdentificationNumber"],
                            ClientName = (string)dr["ClientName"],
                            ClientAddress = clientAddress,
                            ClientCellPhone = clientCellPhone,
                            ClientEmail = clientEmail,
                            BirthDate = stringBirthDate,
                            Gender = (string)dr["Gender"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return client.ToList();
        }

        public IList<ClientViewModel> GetClientByIdentificationNumber(double? idNumber)
        {
            List<ClientViewModel> client = new List<ClientViewModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CLient WHERE IdentificationNumber = " + idNumber, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string clientAddress = string.Empty;
                        string clientCellPhone = string.Empty;
                        string clientEmail = string.Empty;

                        DateTime dateBirthDate = (DateTime)dr["BirthDate"];
                        string stringBirthDate = dateBirthDate.ToString("dd/MM/yyyy");

                        if (dr["ClientAddress"].ToString() != "")
                        {
                            clientAddress = (string)dr["ClientAddress"];
                        }

                        if (dr["ClientCellPhone"].ToString() != "")
                        {
                            clientCellPhone = (string)dr["ClientCellPhone"];
                        }

                        if (dr["ClientEmail"].ToString() != "")
                        {
                            clientEmail = (string)dr["ClientEmail"];
                        }

                        client.Add(new ClientViewModel
                        {
                            ClientId = (int)dr["ClientId"],
                            IdentificationNumber = (double)dr["IdentificationNumber"],
                            ClientName = (string)dr["ClientName"],
                            ClientAddress = clientAddress,
                            ClientCellPhone = clientCellPhone,
                            ClientEmail = clientEmail,
                            BirthDate = stringBirthDate,
                            Gender = (string)dr["Gender"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return client.ToList();
        }
    }
}
