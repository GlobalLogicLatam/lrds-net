using System.ComponentModel.DataAnnotations;

namespace LaRutaDelSoftware.RestAPI.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Datos incorrectos")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Datos incorrectos")]
        [MinLength(6, ErrorMessage = "Datos incorrectos")]
        public string Password { get; set; }
    }
}