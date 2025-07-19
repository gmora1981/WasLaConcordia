using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaConcordia.Model
{
    public class UserEditDTO : IValidatableObject
    {
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo electrónico válida")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; } = string.Empty;

        // Validación personalizada
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Si es un nuevo usuario (sin UserId), la contraseña es requerida
            if (string.IsNullOrEmpty(UserId) && string.IsNullOrEmpty(Password))
            {
                results.Add(new ValidationResult(
                    "La contraseña es requerida para nuevos usuarios",
                    new[] { nameof(Password) }));
            }

            // Si se proporciona contraseña, validar su formato
            if (!string.IsNullOrEmpty(Password))
            {
                // Validar formato de contraseña
                var passwordRegex = @"^(?=(?:.*\d){1})(?=(?:.*[A-Z]){1})(?=(?:.*[a-z]){1})(?=(?:.*[@$><#.,?¡\-_]){1})\S{8,16}$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(Password, passwordRegex))
                {
                    results.Add(new ValidationResult(
                        "La contraseña debe tener entre 8-16 caracteres, incluir mayúsculas, minúsculas, números y caracteres especiales",
                        new[] { nameof(Password) }));
                }

                // Si hay contraseña, debe haber confirmación
                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    results.Add(new ValidationResult(
                        "Debe confirmar la contraseña",
                        new[] { nameof(ConfirmPassword) }));
                }
                // Validar que coincidan
                else if (Password != ConfirmPassword)
                {
                    results.Add(new ValidationResult(
                        "Las contraseñas no coinciden",
                        new[] { nameof(ConfirmPassword) }));
                }
            }

            return results;
        }
    }
}