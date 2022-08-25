using Microsoft.AspNetCore.Mvc;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using System;

namespace Subasta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeilaoController : ControllerBase
    {
        private readonly IServicoLeilao _servico;
        private readonly IUnitOfWork _unitOfWork;

        public LeilaoController(IServicoLeilao servico, IUnitOfWork unitOfWork)
        {
            _servico = servico;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] LeilaoDto leilao)
        {
            try
            {
                _servico.Adicionar(leilao);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("encerrar/{leilaoId}")]
        public IActionResult Encerrar([FromRoute] Guid leilaoId)
        {
            _servico.Encerrar(leilaoId);

            return Ok();
        }
    }
}
