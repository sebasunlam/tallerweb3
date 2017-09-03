using System;
using System.Collections.Generic;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;

namespace ProgramacionWeb3.Servicios.Contracts
{
    public interface IPaqueteServicio : IServicio<Paquete>
    {
        List<Paquete> GetDestacados();
        Page<Paquete> GetListadoPage(int page, int pageSize);
        bool ChangeDestacado(int paqueteId);
    }
}