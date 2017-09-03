using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Models.Extensions;

namespace ProgramacionWeb3.WebApp.Controllers
{
    [RoutePrefix("Paquete")]
    public class PaqueteController : Controller
    {
        private readonly IPaqueteServicio _paqueteServicio;

        public PaqueteController(IPaqueteServicio paqueteServicio)
        {
            _paqueteServicio = paqueteServicio;
        }

        [Route("Detalle/{id}")]
        public ActionResult Detalle(int id)
        {
            var paquete = _paqueteServicio.Get(id);

            if (paquete == null) return View("_notFound");

            return View(paquete.Map());
        }
    }
}