using System;
using System.Linq;

namespace Subasta.Dominio.Interfaces.Repositorios
{
    public interface IRepositorio<TEntidade> where TEntidade : class
    {
        void Adicionar(TEntidade entity);
        IQueryable<TEntidade> Buscar();
        TEntidade BuscarPorId(Guid id);
    }
}
