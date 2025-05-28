using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaConcordia.Model
{
    public class UserEditDTO
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo Electronico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=(?:.*\\d){1})(?=(?:.*[A-Z]){1})(?=(?:.*[a-z]){1})(?=(?:.*[@$><#.,?¡\\-_]){1})\\S{8,16}$", ErrorMessage = "Password muy Debil")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "El campo {0}  es distinto al Campo Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
