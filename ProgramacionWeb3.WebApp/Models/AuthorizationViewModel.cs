using System.ComponentModel.DataAnnotations;

namespace ProgramacionWeb3.WebApp.Models
{
    public class AuthorizationViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un E-Mail")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Ingrese un e-mail del tipo usuario@dominio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}