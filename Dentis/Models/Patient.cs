using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Dentis.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string? PatientName { get; set; } 
        public int PatientAge { get; set; } 
        public string? PatientGender { get; set; } 
        public int AppointmentReasonId { get; set; }
        public string? AppointmentReasonName { get; set; }
    }
    public class PatientViewModel
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string? PatientName { get; set; }
        public int PatientAge { get; set; }
        [Required]
        public string? PatientGender { get; set; }
        [Required]
        public int AppointmentReasonId { get; set; }
        public int ClinicConsultingId { get; set; }
    }
}
