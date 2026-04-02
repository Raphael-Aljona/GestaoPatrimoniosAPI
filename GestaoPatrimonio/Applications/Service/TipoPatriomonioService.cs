using GestaoPatrimonio.Applications.Rules;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.TipoPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Repositories;

namespace GestaoPatrimonio.Applications.Service
{
    public class TipoPatriomonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatriomonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonio> Listar()
        {
            List<TipoPatrimonio> tipoPatrimonios = _repository.Listar();

            List<ListarTipoPatrimonio> tipoPatrimoniosDto = tipoPatrimonios.Select(t => new ListarTipoPatrimonio
            {
                NomeTipo = t.NomeTipo,
                TipoPatrimonioID = t.TipoPatrimonioID,
            }).ToList();

            return tipoPatrimoniosDto;  
        }

        public ListarTipoPatrimonio ObterPorId(Guid id)
        {
            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorId(id);

            if (tipoPatrimonioExistente == null) throw new DomainException("Tipo patrimônio não encontrado");

            ListarTipoPatrimonio tipoPatrimonioDto = new ListarTipoPatrimonio
            {
                NomeTipo = tipoPatrimonioExistente.NomeTipo,
                TipoPatrimonioID = tipoPatrimonioExistente.TipoPatrimonioID
            };

            return tipoPatrimonioDto;
        }

        public void Adicionar (CriarTipoPatrimonio tipoPatrimonioDto)
        {
            Validar.ValidarNome(tipoPatrimonioDto.NomeTipo);

            if (tipoPatrimonioDto == null) throw new DomainException("");

            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorNome(tipoPatrimonioDto.NomeTipo);

            if(tipoPatrimonioExistente != null) throw new DomainException("Já existe um patrimônio com esse nome");

            TipoPatrimonio tipoPatrimonio = new TipoPatrimonio
            {
                NomeTipo = tipoPatrimonioDto.NomeTipo
            };

            _repository.Adicionar(tipoPatrimonio);
        }

        public void Atualizar(CriarTipoPatrimonio tipoPatrimonioDto, Guid id)
        {
            TipoPatrimonio tipoPatrimonioBanco = _repository.BuscarPorId(id);

            if (tipoPatrimonioBanco == null) throw new DomainException("Patrimonio não encontrado");

            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorNome(tipoPatrimonioDto.NomeTipo);

            if (tipoPatrimonioExistente != null && id != tipoPatrimonioExistente.TipoPatrimonioID) throw new DomainException("Já existe um patrimonio com esse nome");

            tipoPatrimonioBanco.NomeTipo = tipoPatrimonioDto.NomeTipo;

            _repository.Atualizar(tipoPatrimonioBanco);
        }
    }
}
