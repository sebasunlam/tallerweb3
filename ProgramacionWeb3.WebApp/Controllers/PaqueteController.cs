using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Models;
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

        [Authorize(Roles = "Administrador"), Route("Index/{page}/{pageSize}"),Route("Index")]
        public ActionResult Index(int page = 1,int pageSize = 10)
        {
            var paquetes = _paqueteServicio.GetListadoPage(page,pageSize);
            return View(new ListadoPaquetesViewModel
            {
                Paquetes = paquetes.Select(x => x.Map()).ToList(),
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

        [Authorize,Route("Reservar/{id}")]
        public ActionResult Reservar(long id)
        {
            var paquete = _paqueteServicio.Get(id);
            return paquete == null ? View("_notFound") : View(paquete.Map());
        }

        [Authorize(Roles = "Administrador"), HttpPost,Route("ChangeDestacado/{paqueteId}")]
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

        [Authorize(Roles = "Administrador"),HttpPost, Route("Eliminar"),ValidateAntiForgeryToken]
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

                return Redirect(Url.Action("Index"));
            }

            return View(model);
        }

        [Authorize(Roles = "Administrador"), Route("Crear")]
        public ActionResult Crear()
        {
            return View(new PaqueteViewModel{FechaInicio = DateTime.Now.Date,FechaFin = DateTime.Now.Date});
        }

        [Authorize(Roles = "Administrador"), HttpPost, Route("Crear"), ValidateAntiForgeryToken]
        public ActionResult Crear(PaqueteViewModel model, HttpPostedFileBase file)
        {
            if(file == null)
                ModelState.AddModelError("Foto","Debe seleccionar una foto para el paquete");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.Foto = ManageFile(file);
                }

                _paqueteServicio.Create(model.Map());

                return Redirect(Url.Action("Index"));
            }

            return View(model);
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