using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.WebApp.Models;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.WebApp.Extensions;

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
            if (!ModelState.IsValid) return View(model);

            var usuario = _usuarioServicio.CheckUserAndPassword(model.Email, model.Password);

            if (usuario != null)
            {
                IdentitySignin(usuario,model.Remember);

                return Redirect(Url.Action("Index", "Home"));
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            IdentitySignout();

            return Redirect(Url.Action("Index", "Home"));
        }


        public void IdentitySignin(Usuario user, bool isPersistent = false)
        {
            var pw3User = user.Map();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, pw3User.Id.ToString()),
                new Claim(ClaimTypes.GivenName, pw3User.ApellidoNombre),
                new Claim(ClaimTypes.Email,pw3User.Email),
                new Claim("apellido",pw3User.Apellido),
                new Claim("nombre",pw3User.Nombre)
            };

            if(user.Admin)
                claims.Add(new Claim(ClaimTypes.Role,"Administrador"));

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