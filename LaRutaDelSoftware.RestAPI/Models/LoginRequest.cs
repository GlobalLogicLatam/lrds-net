using System.ComponentModel.DataAnnotations;

namespace LaRutaDelSoftware.RestAPI.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "La password debe ser de 6 caracteres minímo.")]
        public string Password { get; set; }
    }
}