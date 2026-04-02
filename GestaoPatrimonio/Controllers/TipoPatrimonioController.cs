using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.TipoPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatriomonioService _service;

        public TipoPatrimonioController(TipoPatriomonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonio>> Listar()
        {
            List<ListarTipoPatrimonio> tipoPatrimonios = _service.Listar();
            return Ok(tipoPatrimonios);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoPatrimonio> ObterPorId(Guid id)
        {
            ListarTipoPatrimonio tipoPatrimonio = _service.ObterPorId(id);
            return Ok(tipoPatrimonio);
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoPatrimonio tipoPatrimonioDto)
        {
            try
            {
                _service.Adicionar(tipoPatrimonioDto);
                return StatusCode(202, tipoPatrimonioDto);
            }
            catch (DomainException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarTipoPatrimonio tipoPatrimonioDto, Guid id)
        {
            try
            {
                _service.Atualizar(tipoPatrimonioDto, id);
                return NoContent();
            }catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
