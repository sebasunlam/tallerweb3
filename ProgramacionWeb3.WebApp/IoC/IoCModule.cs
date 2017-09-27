using System.Data.Entity;
using System.Reflection;
using ProgramacionWeb3.Dominio.Contracts;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Implementation;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.Servicios.Implementation;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace ProgramacionWeb3.WebApp.IoC
{
    public class IoCModule
    {
        public static Container Register(bool mvcProject = true)
        {
            
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            #region Servicios
            //Ejemplo de registro de Repositorio custom
            //container.Register(INombreEntidadServicio<NombreEntidad>, NombreEntidadServicio<NombreEntidad>,Lifestyle.Scoped);
            container.Register(typeof(IServicio<>), typeof(Servicio<>), Lifestyle.Scoped);
            container.Register<IUsuarioServicio,UsuarioServicio>(Lifestyle.Scoped);
            container.Register<IPaqueteServicio,PaqueteServicio>(Lifestyle.Scoped);
            container.Register<IReservaServicio, ReservaServicio>(Lifestyle.Scoped);


            #endregion

            #region Repositorios
            //Ejemplo de registro de Repositorio custom
            //container.Register(INombreEntidadRepositorio<NombreEntidad>, NombreEntidadRepositorio<NombreEntidad>,Lifestyle.Scoped);
            container.Register(typeof(IRepositorio<>), typeof(Repositorio<>),Lifestyle.Scoped);
            #endregion

            #region Contexto
            container.Register<IUnitOfWork,UnitOfWork>(Lifestyle.Scoped);
            container.Register<DbContext, PW3_20172C_TP_TurismoEntities>(Lifestyle.Scoped);
            #endregion

            #region Controllers
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            #endregion

            #region Filters
            if(mvcProject)
            container.RegisterMvcIntegratedFilterProvider();
            #endregion

            container.Verify();

            return container;
        }
    }
}