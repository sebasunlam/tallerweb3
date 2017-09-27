using System;
using ProgramacionWeb3.Dominio.Entidades;

namespace ProgramacionWeb3.WebApp.Models.Extensions
{
    public static class ReservaViewModelExtension
    {
        public static ReservaViewModel Map(this Reserva value, Paquete paquete)
        {
            return new ReservaViewModel
            {
                PaqueteViewModel = paquete.Map(),
                CantPersonas = value.CantPersonas,
                FechaCreacion = value.FechaCreacion,
                IdPaquete = paquete.Id,
                Id = value.Id,
                IdUsuario = value.IdUsuario
            };
        }

        public static Reserva Map(this ReservaViewModel value,int idUsuario, Reserva reserva = null)
        {
            var create = false;
            if (reserva == null)
            {
                reserva = new Reserva();
                create = true;
            }

            reserva.IdPaquete = value.IdPaquete;
            reserva.IdUsuario = idUsuario;
            reserva.CantPersonas = value.CantPersonas;

            if (create)
            {
                reserva.FechaCreacion = DateTime.Now;
            }

            return reserva;

        }
        
    }
}