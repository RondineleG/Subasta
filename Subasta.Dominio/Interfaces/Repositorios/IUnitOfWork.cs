using System;

namespace Subasta.Dominio.Interfaces.Repositorios
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
