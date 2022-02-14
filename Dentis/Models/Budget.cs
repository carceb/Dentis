﻿using Newtonsoft.Json;

namespace Dentis.Models
{
    public class BudgetViweModel
    {
        public int BudgetId { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public int BudgetDetailId { get; set; }
        public int QuadrantToothId { get; set; }
        public int QuadrantId { get; set; }
        public string? QuadrantName { get; set; }
        public int ProcedureId { get; set; }
        public string? ProcedureName { get; set; }
        public string? Observation { get; set; }
        public double Cost { get; set; }
        public int ToothNumber { get; set; }
        public int ClinicConsultingId { get; set; }
    }


    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class Customer
    {
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Country")]
        public string Country { get; set; }
    }

    public class RootObject
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string QuadrantId { get; set; }
    }
}
