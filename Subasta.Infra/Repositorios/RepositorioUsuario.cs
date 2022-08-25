using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Infra.Repositorios.Base;
using System.Linq;

namespace Subasta.Infra.Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(DbContext context) : base(context)
        {
        }

        public bool LoginEstaEmUso(string login)
        {
            return _set.Any(x => x.Login == login);
        }
    }
}
