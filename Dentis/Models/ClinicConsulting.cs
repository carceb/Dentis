using System.ComponentModel.DataAnnotations;

namespace Dentis.Models
{
    public class ClinicConsultingViewModel
    {
        [Key]
        public int ClinicConsultingId { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [Required]
        public string? ClinicConsultingName { get; set; }
        public string? ClinicConsultingPhone { get; set; }
        public string? ClinicName { get; set; }
    }
}
