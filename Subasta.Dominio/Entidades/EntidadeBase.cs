using System;

namespace Subasta.Dominio.Entidades
{
    public class EntidadeBase
    {
        public Guid Id { get; private set; }

        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
