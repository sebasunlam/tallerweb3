﻿using System.Linq;
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
                List = _paqueteServicio.GetDestacados().Select(x=>x.Map()).ToList()
            });
        }
    }
}