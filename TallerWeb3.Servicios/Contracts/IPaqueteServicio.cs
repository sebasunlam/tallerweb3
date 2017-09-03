using System;
using System.Collections.Generic;
using ProgramacionWeb3.Dominio.Entidades;

namespace ProgramacionWeb3.Servicios.Contracts
{
    public interface IPaqueteServicio : IServicio<Paquete>
    {
        List<Paquete> GetDestacados();
    }
}