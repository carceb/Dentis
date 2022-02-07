using System.ComponentModel.DataAnnotations;

namespace Dentis.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        public string? ClinicRif { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicAddress { get; set; }
        public string? ClinicPhoneNumber { get; set; }
        public string? ClinicEmail { get; set; }
        public string? WebPage { get; set; }
        public string? ClinicStatus { get; set; }
    }

    public class ClinicViewModel
    {
        [Key]
        public int ClinicId { get; set; }
        [Required]
        public string? ClinicRif { get; set; }
        [Required]
        public string? ClinicName { get; set; }
        [Required]
        public string? ClinicAddress { get; set; }
        [Required]
        public string? ClinicPhoneNumber { get; set; }
        [Required]
        public string? ClinicEmail { get; set; }
        public string? WebPage { get; set; }
        public string? ClinicStatus { get; set; }
    }
}
