using System.ComponentModel.DataAnnotations;

namespace Dentis.Models
{
    public class ClinicConsultingViewModel
    {
        [Key]
        [Required(ErrorMessage = "Debe seleccionar el consultorio")]
        public int ClinicConsultingId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la clinica")]
        public int ClinicId { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del consultorio")]
        public string? ClinicConsultingName { get; set; }

        public string? ClinicConsultingPhone { get; set; }

        public string? ClinicName { get; set; }
    }
}
