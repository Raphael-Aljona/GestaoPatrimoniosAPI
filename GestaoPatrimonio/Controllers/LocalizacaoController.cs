using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.LocalizacaoDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarLocalizacaoDto>> Listar()
        {
            List<ListarLocalizacaoDto> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarLocalizacaoDto> ObterPorId(Guid id)
        {
            try
            {
                ListarLocalizacaoDto localizacao = _service.ObterPorId(id);
                return Ok(localizacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(202, dto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarLocalizacaoDto dto)
        {
            try
            {
                _service.Atualizar(dto, id);
                return StatusCode(204, dto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
