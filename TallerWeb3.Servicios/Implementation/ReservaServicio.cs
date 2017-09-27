using System.Collections.Generic;
using System.Linq;
using ProgramacionWeb3.Dominio.Contracts;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;
using ProgramacionWeb3.Servicios.Contracts;

namespace ProgramacionWeb3.Servicios.Implementation
{
    public class ReservaServicio : Servicio<Reserva>, IReservaServicio
    {
        private readonly IRepositorio<Paquete> _paqueteRepositorio;

        public ReservaServicio(IRepositorio<Reserva> repositorio, IUnitOfWork unitOfWork, IRepositorio<Paquete> paqueteRepositorio) : base(repositorio, unitOfWork)
        {
            _paqueteRepositorio = paqueteRepositorio;
        }

        public override void Create(Reserva item)
        {
            var paquete = _paqueteRepositorio.Get(item.IdPaquete);

            paquete.LugaresDisponibles = paquete.LugaresDisponibles - item.CantPersonas;

            base.Create(item);
        }

        public Page<Reserva> GetAll(int idUsuario, int pageIndex, int pageSize)
        {
            return GetAllItems(x => x.IdUsuario == idUsuario).OrderByDescending(x=>x.FechaCreacion).ToPage(pageIndex, pageSize);
        }
    }
}