using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.AreaDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        public readonly AreaService _service;

        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarAreaDto>> Listar()
        {
            List<ListarAreaDto> areas = _service.Listar();
            return StatusCode(200, areas);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarAreaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarAreaDto areaDto = _service.ListarPorId(id);
                return Ok(areaDto);
            }catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarAreaDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(202, dto);
            }catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarAreaDto dto, Guid id)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
