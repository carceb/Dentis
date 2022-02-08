namespace Dentis.Models
{
    public class BudgetViweModel
    {
        public int BudgetId { get; set; }
        public int ClientId { get; set; }
		public string? ClientName { get; set; }
        public float IdentificationNumber { get; set; }
        public int BudgetDetailId { get; set; }
        public int QuadrantToothId { get; set; }
        public int QuadrantId { get; set; }
        public string? QuadrantName { get; set; }
        public int ProcedureId { get; set; }
        public string? ProcedureName { get; set; }
        public double Cost { get; set; }
        public int ToothNumber { get; set; }
    }
}
