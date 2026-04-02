using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.StatusPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;

        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusPatrimonio>> Listar()
        {
            List<ListarStatusPatrimonio> statusPatrimonio = _service.Listar();
            return Ok(statusPatrimonio);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusPatrimonio> ObterPorId(Guid id)
        {
            try
            {
                ListarStatusPatrimonio listar = _service.ObterPorId(id);
                return Ok(listar);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusPatrimonio criarStatus)
        {
            try
            {
                _service.Adicionar(criarStatus);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarStatusPatrimonio criarStatusPatrimonio, Guid id)
        {
            try
            {
                _service.Atualizar(criarStatusPatrimonio, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
