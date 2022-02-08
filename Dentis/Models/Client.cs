using System.ComponentModel.DataAnnotations;

namespace Dentis.Models
{
    public class ClientViewModel
    {
        [Key]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "La cédula es requrida")]
        public double IdentificationNumber { get; set; }
        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        public string? ClientName { get; set; }
        [Required(ErrorMessage = "El genero es requerido")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateTime BirthDate { get; set; }
        public string? ClientAddress { get; set; }
        public string? ClientCellPhone { get; set; }
        public string? ClientEmail { get; set; }
    }
}
