using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Models;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace ProgramacionWeb3.WebApp.Controllers
{
    [RoutePrefix("Authorization")]
    public class AuthorizationController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public AuthorizationController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous,HttpPost, Route("Login")]
        public ActionResult Login(AuthorizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioServicio.CheckUserAndPassword(model.Email, model.Password);

                if (usuario != null)
                {
                    IdentitySignin(usuario.Map(),model.Remember);

                    return Redirect(Url.Action("Index", "Home"));
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            IdentitySignout();

            return Redirect(Url.Action("Index", "Home"));
        }


        public void IdentitySignin(Pw3User user, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.ApellidoNombre),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("apellido",user.Apellido),
                new Claim("nombre",user.Nombre),
                new Claim("userState", user.ToString())
            };


            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
    }
}