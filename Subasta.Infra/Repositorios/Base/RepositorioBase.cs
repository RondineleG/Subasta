using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Interfaces.Repositorios;
using System;
using System.Linq;

namespace Subasta.Infra.Repositorios.Base
{
    public class RepositorioBase<TEntidade> : IRepositorio<TEntidade> where TEntidade : class
    {
        protected readonly DbContext _contexto;
        protected readonly DbSet<TEntidade> _set;

        protected RepositorioBase(DbContext context)
        {
            _contexto = context;
            _set = _contexto.Set<TEntidade>();
        }

        public void Adicionar(TEntidade entity)
        {
            _contexto.Add(entity);
        }

        public IQueryable<TEntidade> Buscar()
        {
            return _set;
        }

        public TEntidade BuscarPorId(Guid id)
        {
            return _set.Find(id);
        }
    }
}
