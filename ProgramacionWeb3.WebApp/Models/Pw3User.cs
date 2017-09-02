using ProgramacionWeb3.Dominio.Entidades;

namespace ProgramacionWeb3.WebApp.Models
{
    public class Pw3User
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string ApellidoNombre => $"{Apellido}, {Nombre}";
    }

    public static class Pw3UserExtension
    {
        public static Pw3User Map(this Usuario user)
        {
            return new Pw3User
            {
                Apellido = user.Apellido,
                Nombre = user.Nombre,
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}