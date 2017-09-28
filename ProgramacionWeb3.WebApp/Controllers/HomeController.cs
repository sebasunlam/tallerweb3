using System.Linq;
using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Filters;
using ProgramacionWeb3.WebApp.Models;
using ProgramacionWeb3.WebApp.Models.Extensions;

namespace ProgramacionWeb3.WebApp.Controllers
{
    [LayoutFilter]
    public class HomeController : Controller
    {
        private readonly IPaqueteServicio _paqueteServicio;

        public HomeController(IPaqueteServicio paqueteServicio)
        {
            _paqueteServicio = paqueteServicio;
        }

        public ActionResult Index()
        {
            return View(new ListadoPaquetesViewModel
            {
                Paquetes = _paqueteServicio.GetDestacados().Select(x=>x.Map()).ToList()
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