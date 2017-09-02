using System.Security.Claims;
using System.Security.Principal;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.WebApp.Models;

namespace ProgramacionWeb3.WebApp.Extensions
{
    public static class Pw3UserExtension
    {
        public static Pw3User Map(this Usuario user)
        {
            return new Pw3User
            {
                Apellido = user.Apellido,
                Nombre = user.Nombre,
                Id = user.Id,
                Email = user.Email
            };
        }

        public static Pw3User ToPw3User(this IIdentity identity)
        {
            var claims = identity as ClaimsIdentity;

            if (claims == null) return null;

            return new Pw3User
            {
                Apellido = claims.FindFirst(x=>x.Type == "apellido").Value,
                Nombre = claims.FindFirst(x => x.Type == "nombre").Value,
                Email = claims.FindFirst(x => x.Type == ClaimTypes.Email).Value,
                Id = int.Parse(claims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value)
            };
        }
    }
}