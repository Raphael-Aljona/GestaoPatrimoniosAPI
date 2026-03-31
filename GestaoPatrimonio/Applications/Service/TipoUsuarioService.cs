using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.TipoUsuarioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Repositories;

namespace GestaoPatrimonio.Applications.Service
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuario> Listar()
        {
            List<TipoUsuario> tipoUsuario = _repository.Listar();
            List<ListarTipoUsuario> tipoUsuarioDto = tipoUsuario.Select(t => new ListarTipoUsuario
            {
                NomeTipo = t.NomeTipo,
                TipoUsuarioID = t.TipoUsuarioID,
            }).ToList();

            return tipoUsuarioDto;
        }

        public ListarTipoUsuario ObterPorId(Guid id)
        {
            TipoUsuario tipoUsuario = _repository.BuscarPorId(id);

            if (tipoUsuario == null) throw new DomainException("Tipo Usuário não encontrado.");

            ListarTipoUsuario tipoUsuarioDto = new ListarTipoUsuario
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo,
            };

            return tipoUsuarioDto;
        }

        public void Adicionar(CriarTipoUsuario tipoUsuarioDto)
        {
            TipoUsuario usuarioExiste = _repository.BuscarPorNome(tipoUsuarioDto.NomeTipo);

            if (usuarioExiste != null) throw new DomainException("Esse tipo de usuário existe");

            TipoUsuario tipoUsuario = new TipoUsuario
            {
                NomeTipo = tipoUsuarioDto.NomeTipo
            };

            _repository.Adicionar(tipoUsuario);
        }

        public void Atualizar(CriarTipoUsuario tipoUsuarioDto, Guid id)
        {
            TipoUsuario usuarioBanco = _repository.BuscarPorId(id);

            if (usuarioBanco == null) throw new DomainException("Usuário não encontrado");

            TipoUsuario usuarioExistente = _repository.BuscarPorNome(tipoUsuarioDto.NomeTipo);

            if (usuarioExistente != null) throw new DomainException("Já existe um tipo de usuário com esse nome");

            usuarioBanco.NomeTipo = tipoUsuarioDto.NomeTipo;

            _repository.Atualizar(usuarioBanco);
        }
    }
}
