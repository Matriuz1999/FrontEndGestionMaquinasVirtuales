using System.ComponentModel.DataAnnotations;

namespace FrontEndGestionMaquinasVirtuales.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
    }
}
