using System.Data.Entity;
using System.Reflection;
using ProgramacionWeb3.Dominio.Contracts;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Repositorios;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.Servicios.Implementation;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace ProgramacionWeb3.WebApp.IoC
{
    public class IoCModule
    {
        public static Container Register()
        {
            
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            #region Servicios
            //Ejemplo de registro de Repositorio custom
            //container.Register(INombreEntidadServicio<NombreEntidad>, NombreEntidadServicio<NombreEntidad>,Lifestyle.Scoped);
            container.Register(typeof(IServicio<>), typeof(Servicio<>), Lifestyle.Scoped);


            #endregion

            #region Repositorios
            //Ejemplo de registro de Repositorio custom
            //container.Register(INombreEntidadRepositorio<NombreEntidad>, NombreEntidadRepositorio<NombreEntidad>,Lifestyle.Scoped);
            container.Register(typeof(IRepositorio<>), typeof(Repositorio<>),Lifestyle.Scoped);
            #endregion

            #region Contexto
            container.Register<DbContext, PW3_20172C_TP_TurismoEntities>(Lifestyle.Scoped);
            #endregion

            #region Controllers
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            #endregion

            #region Filters
            container.RegisterMvcIntegratedFilterProvider();
            #endregion

            container.Verify();

            return container;
        }
    }
}