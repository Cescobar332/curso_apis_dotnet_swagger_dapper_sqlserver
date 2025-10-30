using System.ComponentModel.DataAnnotations;

namespace CursoUdemyWebAPI.DTO
{
    public class EmpleadoDTO
    {
        [Required(ErrorMessage = "Obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [MaxLength(4, ErrorMessage = "Máximo 4 dígitos")]
        public string CodEmpleado { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [EmailAddress(ErrorMessage = "Formato incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [Range(18, 100, ErrorMessage = "La edad debe estar entre los 18 y los 100 años")]
        public int Edad { get; set; }
    }
}
