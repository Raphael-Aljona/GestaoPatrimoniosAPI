using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;
        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarSolicitacaoTransferenciaDto>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDto> dtos = _service.Listar();
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<ListarSolicitacaoTransferenciaDto> BuscarPorID(Guid id)
        {
            try
            {
                ListarSolicitacaoTransferenciaDto dto = _service.BuscarPorId(id);
                return Ok(dto);
            }catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
