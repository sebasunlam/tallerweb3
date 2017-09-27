using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Implementation;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.Servicios.Implementation;
using ProgramacionWeb3.WebApp.Extensions;

namespace ProgramacionWeb3.WebApp.Controllers
{
    [RoutePrefix("api/reserva")]
    public class ReservaController : ApiController
    {
        [HttpPost,Authorize,Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var context = new PW3_20172C_TP_TurismoEntities();
            var reservaServicio  = new ReservaServicio(new Repositorio<Reserva>(context), new UnitOfWork(context),new Repositorio<Paquete>(context) );
            var reserva = reservaServicio.Get(id);

            if (reserva == null) return NotFound();

            if (reserva.IdUsuario != User.Identity.ToPw3User().Id) return BadRequest();

            reservaServicio.Delete(id);

            return Ok();
        }
    }
}
