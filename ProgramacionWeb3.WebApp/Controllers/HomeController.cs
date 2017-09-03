using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Models;

namespace ProgramacionWeb3.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaqueteServicio _paqueteServicio;

        public HomeController(IPaqueteServicio paqueteServicio)
        {
            _paqueteServicio = paqueteServicio;
        }

        public ActionResult Index()
        {
            return View(new HomePaquetesViewModel
            {
                Paquetes = _paqueteServicio.GetDestacados()
            });
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}