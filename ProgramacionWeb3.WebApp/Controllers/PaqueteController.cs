using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Extensions;
using ProgramacionWeb3.WebApp.Filters;
using ProgramacionWeb3.WebApp.Models;
using ProgramacionWeb3.WebApp.Models.Extensions;

namespace ProgramacionWeb3.WebApp.Controllers
{
    [RoutePrefix("Paquete")]
    [LayoutFilter]
    public class PaqueteController : Controller
    {
        private readonly IPaqueteServicio _paqueteServicio;
        private readonly IReservaServicio _reservaServicio;

        public PaqueteController(IPaqueteServicio paqueteServicio, IReservaServicio reservaServicio)
        {
            _paqueteServicio = paqueteServicio;
            _reservaServicio = reservaServicio;
        }

        [Authorize(Roles = "Administrador"), Route("Index/{page}/{pageSize}"), Route("Index")]
        public ActionResult Index(int page = 1, int pageSize = 10, string paquete = null)
        {
            var paquetes = _paqueteServicio.GetListadoPage(page, pageSize);
            ViewBag.paqueteOperacion = paquete;
            return View(new ListadoPaquetesViewModel
            {
                List = paquetes.Select(x => x.Map()).ToList(),
                TotalItems = paquetes.TotalItems,
                TotalPages = paquetes.TotalPages,
                CurrentPage = page,
                PageSize = pageSize
            });
        }

        [Route("Detalle/{id}")]
        public ActionResult Detalle(int id)
        {
            var paquete = _paqueteServicio.Get(id);

            return paquete == null ? View("_notFound") : View(paquete.Map());
        }

        [Authorize(Roles = "Administrador"), HttpPost, Route("ChangeDestacado/{paqueteId}")]
        public JsonResult ChangeDestacado(int paqueteId)
        {
            return Json(_paqueteServicio.ChangeDestacado(paqueteId), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador"), Route("Eliminar/{id}")]
        public ActionResult Eliminar(int id)
        {
            var paquete = _paqueteServicio.Get(id);
            return paquete == null ? View("_notFound") : View(paquete.Map());
        }

        [Authorize(Roles = "Administrador"), HttpPost, Route("Eliminar"), ValidateAntiForgeryToken]
        public ActionResult Eliminar(PaqueteViewModel model)
        {
            var paquete = _paqueteServicio.Get(model.Id);

            if (paquete == null) return View("_notFound");

            _paqueteServicio.Delete(paquete.Id);

            return Redirect(Url.Action("Index"));
        }

        [Authorize(Roles = "Administrador"), Route("Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var paquete = _paqueteServicio.Get(id);
            return paquete == null ? View("_notFound") : View(paquete.Map());
        }

        [Authorize(Roles = "Administrador"), HttpPost, Route("Editar"), ValidateAntiForgeryToken]
        public ActionResult Editar(PaqueteViewModel model, HttpPostedFileBase file)
        {
            var paquete = _paqueteServicio.Get(model.Id);

            if (paquete == null) return View("_notFound");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.Foto = ManageFile(file);
                }

                _paqueteServicio.Update(model.Map(paquete));

                return Redirect(Url.Action("Index", new { paquete = $"Paquete {model.Nombre} modificado con exito" }));
            }

            return View(model);
        }

        [Authorize(Roles = "Administrador"), Route("Crear")]
        public ActionResult Crear()
        {
            return View(new PaqueteViewModel { FechaInicio = DateTime.Now.Date, FechaFin = DateTime.Now.Date });
        }

        [Authorize(Roles = "Administrador"), HttpPost, Route("Crear"), ValidateAntiForgeryToken]
        public ActionResult Crear(PaqueteViewModel model, HttpPostedFileBase file)
        {
            if (file == null)
                ModelState.AddModelError("Foto", "Debe seleccionar una foto para el paquete");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.Foto = ManageFile(file);
                }

                _paqueteServicio.Create(model.Map());

                return Redirect(Url.Action("Index", new { paquete = $"Paquete {model.Nombre} creado con exito" }));
            }

            return View(model);
        }

        [Authorize, Route("Reservar")]
        public ActionResult Reservar(int id)
        {
            var paquete = _paqueteServicio.Get(id);

            return paquete == null ? View("_notFound") : View(new ReservaViewModel().Map(paquete));
        }

        [Authorize, HttpPost, Route("Reservar")]
        public ActionResult Reservar(ReservaViewModel model)
        {
            var paquete = _paqueteServicio.Get(model.IdPaquete);

            if (paquete == null) return View("_notFound");

            if (DateTime.Now > paquete.FechaInicio)
                ModelState.AddModelError("paquete_fechas", "La fecha de inicio del paquete debe ser menor o igual a la fecha actual");

            if (paquete.LugaresDisponibles != null && paquete.LugaresDisponibles < model.CantPersonas)
                ModelState.AddModelError("paquete_disponibles", "La cantidad de pasajeros supera los lugares disponibles");

            if (ModelState.IsValid)
            {
                _reservaServicio.Create(model.Map(User.Identity.ToPw3User().Id));

                return Redirect(Url.Action("Index", "Home"));
            }

            model.PaqueteViewModel = paquete.Map();

            return View(model);
        }

        [Authorize, Route("HistorialReserva/{page}/{pageSize}"), Route("HistorialReserva")]
        public ActionResult HistorialReserva(int page = 1, int pageSize = 10)
        {
            var rervas = _reservaServicio.GetAll(User.Identity.ToPw3User().Id, page, pageSize);



            return View(new ListadoReservasViewModel
            {
                List = rervas.Select(x => x.Map(x.Paquete)).ToList(),
                TotalItems = rervas.TotalItems,
                TotalPages = rervas.TotalPages,
                CurrentPage = page,
                PageSize = pageSize
            });


        }


        private string ManageFile(HttpPostedFileBase file)
        {
            var targetFolder = HttpContext.Server.MapPath("~/imagenes");
            var targetPath = Path.Combine(targetFolder, file.FileName);
            file.SaveAs(targetPath);
            return $"/imagenes/{file.FileName}";
        }

    }
}