using System.Collections.Generic;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;

namespace ProgramacionWeb3.Servicios.Contracts
{
    public interface IReservaServicio : IServicio<Reserva>
    {
        Page<Reserva> GetAll(int idUsuario, int pageIndex, int pageSize);
    }
}