using GestaoPatrimonio.Applications.Service;
using GestaoPatrimonio.DTOs.AutenticacaoDto;
using GestaoPatrimonio.DTOs.UsuarioDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDto dto)
        {
            try
            {
                TokenDto token = _service.Login(dto);
                return StatusCode(201, dto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("trocar-senha")]
        public ActionResult TrocarSenha(ListarUsuarioDto usuarioDto, TrocarPrimeiraSenhaDto trocarSenhaDto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuári não autenticado");
                }

                Guid usuarioId = Guid.Parse(usuarioIdClaim);
                _service.TrocarPrimeiraSenha(usuarioId, trocarSenhaDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
