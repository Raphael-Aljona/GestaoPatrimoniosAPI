using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.LogPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPatrimonioController : ControllerBase
    {
        private readonly LogPatrimonioService _service;

        public LogPatrimonioController(LogPatrimonioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarLogPatrimonioDto>> Listar()
        {
            List<ListarLogPatrimonioDto> logs = _service.Listar();
            return Ok(logs);
        }

        [Authorize]
        [HttpGet("{îd}")]
        public ActionResult<List<ListarLogPatrimonioDto>> ListarId(Guid patrimonioId)
        {
            try
            {
                List<ListarLogPatrimonioDto> dtos = _service.BuscarPorPatrimonio(patrimonioId);
                return Ok(dtos);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
