using Microsoft.AspNetCore.Mvc;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Servicos;
using System;

namespace Subasta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoUsuario _servico;

        public UsuarioController(IServicoUsuario servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] UsuarioDto usuario)
        {
            try
            {
                _servico.Adicionar(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok();
        }
    }
}
