using System.Linq;
using ProgramacionWeb3.Dominio.Contracts;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Servicios.Contracts;

namespace ProgramacionWeb3.Servicios.Implementation
{
    public class UsuarioServicio : Servicio<Usuario>, IUsuarioServicio
    {
        public UsuarioServicio(IRepositorio<Usuario> repositorio, IUnitOfWork unitOfWork) : base(repositorio, unitOfWork)
        {
        }

        public Usuario CheckUserAndPassword(string email, string password)
        {
            return GetAllItems(x => x.Email == email && x.Contrasenia == password).SingleOrDefault();
        }
    }
}