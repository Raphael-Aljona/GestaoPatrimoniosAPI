using GestaoPatrimonio.Applications.Rules;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.StatusPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonio> Listar()
        {
            List<StatusPatrimonio> statusPatrimonio = _repository.Listar();
            List<ListarStatusPatrimonio> statusPatrimoniosDto = statusPatrimonio.Select(s => new ListarStatusPatrimonio
            {
                NomeStatus = s.NomeStatus,
                StatusPatrimonioID = s.StatusPatrimonioID,
            }).ToList();

            return statusPatrimoniosDto;
        }

        public ListarStatusPatrimonio ObterPorId(Guid id)
        {
            StatusPatrimonio statusPatrimonio = _repository.BuscarPorId(id);

            if (statusPatrimonio == null) throw new DomainException("Status Patrimonio não encontrado");

            ListarStatusPatrimonio statusPatrimonioDto = new ListarStatusPatrimonio
            {
                NomeStatus = statusPatrimonio.NomeStatus,
                StatusPatrimonioID = statusPatrimonio.StatusPatrimonioID
            };
            return statusPatrimonioDto;
        }

        public void Adicionar(CriarStatusPatrimonio criarStatusPatrimonio)
        {
            Validar.ValidarNome(criarStatusPatrimonio.NomeStatus);

            StatusPatrimonio statusPatrimonioExiste = _repository.BuscarPorNome(criarStatusPatrimonio.NomeStatus);

            if (statusPatrimonioExiste != null) throw new DomainException("Já existe um patrimônio com esse nome");

            StatusPatrimonio statusPatrimonio = new StatusPatrimonio
            {
                NomeStatus = criarStatusPatrimonio.NomeStatus
            };

            _repository.Adicionar(statusPatrimonio);
        }

        public void Atualizar(CriarStatusPatrimonio criarStatusPatrimonio, Guid id)
        {
            Validar.ValidarNome(criarStatusPatrimonio.NomeStatus);

            StatusPatrimonio statusPatrimonioBanco = _repository.BuscarPorId(id);

            if (statusPatrimonioBanco == null) throw new DomainException("O status patrimonio não foi encontrado");

            StatusPatrimonio statusPatrimonioExistente = _repository.BuscarPorNome(criarStatusPatrimonio.NomeStatus);

            if (statusPatrimonioExistente != null && id != statusPatrimonioBanco.StatusPatrimonioID) throw new DomainException("Já existe um status patrimonio com esse nome");

            statusPatrimonioBanco.NomeStatus = criarStatusPatrimonio.NomeStatus;

            _repository.Atualizar(statusPatrimonioBanco);
        }
    }
}
