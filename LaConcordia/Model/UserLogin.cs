using System.ComponentModel.DataAnnotations;

namespace LaConcordia.Model
{
    public class UserLogin
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo Electronico")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
    }
}
