using System.ComponentModel.DataAnnotations;

namespace LaConcordia.Model
{
    public class ChangeMyPasswordDTO
    {
        [Required(ErrorMessage = "Ingrese su contraseña actual")]
        public string CurrentPassword { get; set; } = "";

        [Required(ErrorMessage = "Ingrese la nueva contraseña")]
        [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres")]
        public string NewPassword { get; set; } = "";

        [Required(ErrorMessage = "Confirme la nueva contraseña")]
        [Compare(nameof(NewPassword), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = "";
    }
}
