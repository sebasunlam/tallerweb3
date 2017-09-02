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

  
}