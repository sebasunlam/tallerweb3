using System;
using System.Collections.Generic;
using System.Linq;
using ProgramacionWeb3.Dominio.Contracts;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;
using ProgramacionWeb3.Servicios.Contracts;

namespace ProgramacionWeb3.Servicios.Implementation
{
    public class PaqueteServicio : Servicio<Paquete>, IPaqueteServicio
    {
        public PaqueteServicio(IRepositorio<Paquete> repositorio, IUnitOfWork unitOfWork) : base(repositorio, unitOfWork)
        {
        }

        public List<Paquete> GetDestacados()
        {
            return GetAllItems(x => x.Destacado && x.FechaInicio > DateTime.Now).OrderBy(x => x.FechaInicio).ToList();
        }

        public Page<Paquete> GetListadoPage(int page,int pageSize)
        {
            return GetAllItems().OrderByDescending(x => x.FechaInicio).ToPage(page, pageSize);
        }

        public bool ChangeDestacado(int paqueteId)
        {
            var paquete = Get(paqueteId);
            if (paquete == null) return false;

            paquete.Destacado = !paquete.Destacado;
            Update(paquete);

            return paquete.Destacado;
        }
    }
}