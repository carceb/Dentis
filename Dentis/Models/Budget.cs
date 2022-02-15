using Newtonsoft.Json;

namespace Dentis.Models
{
    public class BudgetViweModel
    {
        public int BudgetId { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientCellPhone { get; set; }
        public string? ClientEmail { get; set; }
        public int BudgetDetailId { get; set; }
        public int QuadrantToothId { get; set; }
        public int QuadrantId { get; set; }
        public string? QuadrantName { get; set; }
        public int ProcedureId { get; set; }
        public string? ProcedureName { get; set; }
        public string? Observation { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalBudget { get; set; }
        public int ToothNumber { get; set; }
        public int ClinicConsultingId { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicConsultingName { get; set; }
        public string? ClinicConsultingPhone { get; set; }
        public DateTime BudgetDate { get; set; }
    }
}
