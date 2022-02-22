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

        [Required(ErrorMessage = "Debe colocar el RIF")]
        public string? ClinicRif { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre de la clinica")]
        public string? ClinicName { get; set; }

        [Required(ErrorMessage = "Debe colocar la dirección")]
        public string? ClinicAddress { get; set; }

        [Required(ErrorMessage = "Debe colocar el telefono")]
        public string? ClinicPhoneNumber { get; set; }

        [Required(ErrorMessage = "Debe colocar el email")]
        public string? ClinicEmail { get; set; }

        public string? WebPage { get; set; }
        public string? ClinicStatus { get; set; }
    }
}
