using Microsoft.AspNetCore.Mvc;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using System;

namespace Subasta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IServicoItem _servicoItem;
        private readonly IUnitOfWork _unitOfWork;

        public ItemController(IServicoItem servicoUsuario, IUnitOfWork unitOfWork)
        {
            _servicoItem = servicoUsuario;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ItemDto item)
        {
            try
            {
                _servicoItem.Adicionar(item);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost("lance")]
        public IActionResult AdicionarLance([FromBody] LanceDto lance)
        {
            try
            {
                _servicoItem.AdicionarLance(lance);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("encerrar/{id}")]
        public IActionResult Encerrar([FromRoute] Guid id)
        {
            _servicoItem.Encerrar(id);
            return Ok();
        }
    }
}
