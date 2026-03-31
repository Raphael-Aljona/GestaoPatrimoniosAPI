using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.TipoUsuarioDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;

        public TipoUsuarioController(TipoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoUsuario>> Listar()
        {
            List<ListarTipoUsuario> tipoUsuario = _service.Listar();
            return StatusCode(200, tipoUsuario);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoUsuario> ObterPorId(Guid id)
        {
            try
            {
                ListarTipoUsuario tipoUsuario = _service.ObterPorId(id);
                return StatusCode(200, tipoUsuario);
            }catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoUsuario dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(202, dto);    
            } catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }    
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarTipoUsuario dto, Guid id)
        {
            try
            {
                _service.Atualizar(dto, id);
                return StatusCode(204, dto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
