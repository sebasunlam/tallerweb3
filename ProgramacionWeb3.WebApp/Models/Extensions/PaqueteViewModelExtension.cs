using ProgramacionWeb3.Dominio.Entidades;

namespace ProgramacionWeb3.WebApp.Models.Extensions
{
    public static class PaqueteViewModelExtension
    {
        public static PaqueteViewModel Map(this Paquete value)
        {
            return new PaqueteViewModel
            {
                Nombre = value.Nombre,
                FechaFin = value.FechaFin,
                FechaInicio = value.FechaInicio,
                Id = value.Id,
                Descripcion = value.Descripcion,
                Destacado = value.Destacado,
                Foto = value.Foto,
                LugaresDisponibles = value.LugaresDisponibles,
                PrecioPorPersona = value.PrecioPorPersona,
                Reserva = value.Reserva
            };
        }
    }
}