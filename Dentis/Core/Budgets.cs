using Dentis.Models;
using System.Data.SqlClient;
using static Dentis.Core.Interfaces;

namespace Dentis.Core
{
    public class Budgets : IBudget
    {
        private IConfiguration _configuration;
        public Budgets(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public int SaveBudget(BudgetViweModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("BudgetAddOrEdit", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BudgetId", model.BudgetId);
                    cmd.Parameters.AddWithValue("ClientId", model.ClientId);
                    cmd.Parameters.AddWithValue("BudgetDetailId", model.BudgetDetailId);
                    cmd.Parameters.AddWithValue("QuadrantToothId", model.QuadrantToothId);
                    cmd.Parameters.AddWithValue("ProcedureId", model.ProcedureId);
                    cmd.Parameters.AddWithValue("Cost", model.Cost);
                    cmd.Parameters.AddWithValue("ClinicConsultingID", model.ClinicConsultingId);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList<BudgetViweModel> GetBudgetDetailByBudgetIdAndClinicConsultingId(int budgetId, int clinicConsultingId)
        {
            List<BudgetViweModel> budgets = new List<BudgetViweModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM BudgetView WHERE BudgetId = " + budgetId + " AND ClinicConsultingId = " + clinicConsultingId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        budgets.Add(new BudgetViweModel
                        {
                            BudgetId = (int)dr["BudgetId"],
                            BudgetDetailId = (int)dr["BudgetDetailId"],
                            ClientId = (int)dr["ClientId"],
                            ClinicConsultingId = clinicConsultingId,
                            ClientName = (string)dr["ClientName"],
                            QuadrantName = (string)dr["QuadrantName"],
                            QuadrantId = (int)dr["QuadrantId"],
                            ToothNumber = (int)dr["ToothNumber"],
                            ProcedureName = (string)dr["ProcedureName"],
                            Cost = (double)dr["Cost"],
                            Observation = (string)dr["Observation"],
                            ProcedureId = (int)dr["ProcedureId"],
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return budgets.ToList();
        }

        public IList<BudgetViweModel> GetQuadrants()
        {
            List<BudgetViweModel> quadrants = new List<BudgetViweModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Quadrant", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        quadrants.Add(new BudgetViweModel
                        {
                            QuadrantId = (int)dr["QuadrantId"],
                            QuadrantName = (string)dr["QuadrantName"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return quadrants.ToList();
        }

        public IList<BudgetViweModel> GetQuadrantTooth(int quadrantId)
        {
            List<BudgetViweModel> quadrantTooths = new List<BudgetViweModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT dbo.QuadrantTooth.QuadrantToothId, dbo.QuadrantTooth.QuadrantId, dbo.QuadrantTooth.ToothNumber FROM  dbo.Quadrant INNER JOIN  dbo.QuadrantTooth ON dbo.Quadrant.QuadrantId = dbo.QuadrantTooth.QuadrantId" +
                        " WHERE dbo.QuadrantTooth.QuadrantId = " + quadrantId, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        quadrantTooths.Add(new BudgetViweModel
                        {
                            ToothNumber = (int)dr["ToothNumber"],
                            QuadrantToothId = (int)dr["QuadrantToothId"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return quadrantTooths.ToList();
        }

        public IList<BudgetViweModel> GetProcedures()
        {
            List<BudgetViweModel> procedures = new List<BudgetViweModel>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[Procedure]", sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        procedures.Add(new BudgetViweModel
                        {
                            ProcedureId = (int)dr["ProcedureId"],
                            ProcedureName = (string)dr["ProcedureName"]
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return procedures.ToList();
        }
    }
}
