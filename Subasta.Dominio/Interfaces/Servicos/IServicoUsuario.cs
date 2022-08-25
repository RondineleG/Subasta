using Subasta.Dominio.Dtos;

namespace Subasta.Dominio.Interfaces.Servicos
{
    public interface IServicoUsuario
    {
        void Adicionar(UsuarioDto usuarioDto);
    }
}
