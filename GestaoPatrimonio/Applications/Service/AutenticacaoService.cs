using GestaoPatrimonio.Applications.Authentication;
using GestaoPatrimonio.Applications.Rules;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.AutenticacaoDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt tokenJwt)
        {
            _repository = repository;
            _tokenJwt = tokenJwt;
        }

        public static bool VerificarSenha(string senha, byte[] senhaHash)
        {
            var hashDigitado = CriptografiaUsuario.CriptografarSenha(senha);

            return hashDigitado.SequenceEqual(senhaHash);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorNIFComTipoUsuario(loginDto.NIF);

            if (usuario == null) throw new DomainException("NIF ou senha incorretos.");
            if (usuario.Ativo == false) throw new DomainException("Usuário inativo");
            if (!VerificarSenha(loginDto.senha, usuario.Senha)) throw new DomainException("NIF ou senha inválidos");

            string token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto
            {
                Token = token,  
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.NomeTipo
            };

            return novoToken;
        }

        public void TrocarPrimeiraSenha(Guid usuarioId, TrocarPrimeiraSenhaDto dto)
        {
            Validar.ValidarSenha(dto.SenhaAtual);
            Validar.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null) throw new DomainException("Usuário não foi encontrado");

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha)) throw new DomainException("Senha atual inválida");

            if (dto.SenhaAtual == dto.NovaSenha) throw new DomainException("A nova senha deve ser diferente da senha atual.");

            usuario.Senha = CriptografiaUsuario.CriptografarSenha(dto.NovaSenha);

            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }
    }
}
