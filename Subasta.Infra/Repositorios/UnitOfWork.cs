using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Interfaces.Repositorios;

namespace Subasta.Infra.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext _contexto;

        public UnitOfWork(DbContext contexto)
        {
            _contexto = contexto;
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
