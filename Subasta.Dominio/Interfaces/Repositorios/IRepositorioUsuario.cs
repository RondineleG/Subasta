using Subasta.Dominio.Entidades;

namespace Subasta.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        bool LoginEstaEmUso(string login);
    }
}
