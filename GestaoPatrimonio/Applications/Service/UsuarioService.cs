using GestaoPatrimonio.Applications.Authentication;
using GestaoPatrimonio.Applications.Rules;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.UsuarioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<ListarUsuarioDto> usuariosDto = usuarios.Select(u => new ListarUsuarioDto
            {
                UsuarioID = u.UsuarioID,
                NIF = u.NIF,
                Nome = u.Nome,
                RG = u.RG,
                CPF = u.CPF,
                CarteiraTrabalho = u.CarteiraTrabalho,
                Email = u.Email,
                Ativo = u.Ativo,
                PrimeiroAcesso = u.PrimeiroAcesso,
                TipoUsuarioID = u.TipoUsuarioID,
                CargoID = u.CargoID,
                EnderecoID = u.EnderecoID,
            }).ToList();

            return usuariosDto;
        }

        public ListarUsuarioDto ObterPorId(Guid id)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(id);

            if (usuarioBanco == null) throw new DomainException("Usuário não encontrado");

            ListarUsuarioDto usuarioDto = new ListarUsuarioDto
            {
                CPF = usuarioBanco.CPF,
                Ativo = usuarioBanco.Ativo,
                CargoID = usuarioBanco.CargoID,
                CarteiraTrabalho = usuarioBanco.CarteiraTrabalho,
                Email = usuarioBanco.Email,
                EnderecoID = usuarioBanco.EnderecoID,
                NIF = usuarioBanco.NIF,
                Nome = usuarioBanco.Nome,
                PrimeiroAcesso = usuarioBanco.PrimeiroAcesso,
                RG = usuarioBanco.RG,
                TipoUsuarioID = usuarioBanco.TipoUsuarioID,
                UsuarioID = usuarioBanco.UsuarioID
            };

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF) throw new DomainException("Já existe um usuário cadastrado com esse NIF");
                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower()) throw new DomainException("Já existe um usuário cadastrado com esse Email");
                if (usuarioDuplicado.CPF == dto.CPF) throw new DomainException("Já existe um usuário cadastrado com esse CPF");

            }

            if (!_repository.EnderecoExiste(dto.EnderecoID)) throw new DomainException("Endereço informado não existe");

            if (!_repository.CargoExiste(dto.CargoID)) throw new DomainException("Cargo informado não exite");

            if (_repository.TipoUsuarioExiste(dto.TipoUsuarioID)) throw new DomainException("Tipo Usuário informado não existe");

            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                CPF = dto.CPF,
                Nome = dto.Nome,    
                CarteiraTrabalho = dto.CarteiraTrabalho,
                RG = dto.RG,    
                Senha = CriptografiaUsuario.CriptografarSenha(dto.NIF),
                Email = dto.Email,  
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = dto.EnderecoID,
                CargoID = dto.CargoID,
                TipoUsuarioID = dto.TipoUsuarioID,  
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid id, CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(id);

            if (usuarioBanco == null) throw new DomainException("Usuário não encontrado");

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email);

            if(usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF) throw new DomainException("Já existe um usuário cadastrado com esse NIF");
                if (usuarioDuplicado.CPF == dto.CPF) throw new DomainException("Já existe um usuário cadastrado com esse CPF");
                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower()) throw new DomainException("Já existe um usuário com esse Email");
            }

            if (_repository.EnderecoExiste(dto.EnderecoID)) throw new DomainException("Endereço informado não existe");
            if (_repository.CargoExiste(dto.CargoID)) throw new DomainException("Cargo informado não existe");
            if (_repository.TipoUsuarioExiste(dto.TipoUsuarioID)) throw new DomainException("Tipo de usuário informado não existe");

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.Nome = dto.Nome;
            usuarioBanco.RG = dto.RG;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraTrabalho = dto.CarteiraTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoID = dto.EnderecoID;
            usuarioBanco.CargoID = dto.CargoID;
            usuarioBanco.TipoUsuarioID = dto.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);
        }

        public void AtualizarStatus(AtualizarStatusUsuarioDto usuarioDto, Guid usuarioId)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null) throw new DomainException("Usuário não encontrado");

            usuarioBanco.Ativo = usuarioDto.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }
    }
}
