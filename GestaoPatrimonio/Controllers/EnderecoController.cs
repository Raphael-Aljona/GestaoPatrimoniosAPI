using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.EnderecoDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public ActionResult<List<ListarEnderecoDto>> Listar()
        {
            List<ListarEnderecoDto> enderecos = _enderecoService.Listar();
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarEnderecoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarEnderecoDto endereco = _enderecoService.ListarPorId(id);
                return Ok(endereco);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarEnderecoDto endereco)
        {
            try
            {
                _enderecoService.Adicionar(endereco);
                return StatusCode(204, endereco);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarEnderecoDto dto)
        {
            try
            {
                _enderecoService.Atualizar(id, dto);
                return StatusCode(204, dto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
