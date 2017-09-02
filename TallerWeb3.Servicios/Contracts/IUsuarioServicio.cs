using ProgramacionWeb3.Dominio.Entidades;

namespace ProgramacionWeb3.Servicios.Contracts
{
    public interface IUsuarioServicio : IServicio<Usuario>
    {
        Usuario CheckUserAndPassword(string email, string password);
    }
}