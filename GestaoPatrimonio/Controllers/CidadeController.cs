using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.CidadeDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController (CidadeService service)
        {
            _service = service; 
        }

        [HttpGet]
        public ActionResult<ListarCidadeDto> Listar()
        {
            List<ListarCidadeDto> cidadeDtos = _service.Listar();
            return Ok(cidadeDtos);
        }
    }
}
