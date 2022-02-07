namespace Dentis.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public float IdentificationNumber { get; set; }
        public string? ClientName { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ClientAddress { get; set; }
        public string? ClientCellPhone { get; set; }
        public string? ClientEmail { get; set; }
    }
}
