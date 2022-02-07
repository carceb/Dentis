using System.ComponentModel.DataAnnotations;

namespace Dentis.Models
{
    public class SecurityUserModel
    {
        [Key]
        public int SecurityUserId { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string? UserLogin { get; set; }
        [Required(ErrorMessage = "La clave es requerida")]
        public string? UserPassword { get; set; }
        [Required]
        public string? SecurityUserName { get; set; }
        public string? SecurityUserStatus { get; set; }
        public int SecurityUserTypeId { get; set; }
        public int ClinicConsultingId { get; set; }
        public int ClinicId { get; set; }
        public string? ClinicName { get; set; }
    }
}
