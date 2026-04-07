using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.UsuarioDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ListarUsuarioDto>> Listar()
        {
            List<ListarUsuarioDto> usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarUsuarioDto> ObterPorId(Guid id)
        {
            try
            {
                ListarUsuarioDto usuarioDto = _service.ObterPorId(id);
                return Ok(usuarioDto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public ActionResult Adicionar(CriarUsuarioDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarUsuarioDto dto, Guid id)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPatch("{id}/status")]
        public ActionResult AtualizarStatus(AtualizarStatusUsuarioDto dto, Guid id)
        {
            try
            {
                _service.AtualizarStatus(dto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
