using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository uRepository)
        {
            _repository = repository;
            _usuarioRepository = uRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();
            List<ListarSolicitacaoTransferenciaDto> solicitacoesDto = solicitacoes.Select(s => new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = s.TransferenciaID,
                DataCriacaoSolicitante = s.DataCriacaoSolicitante,
                DataResposta = s.DataResposta,
                Justificativa = s.Justificativa,
                StatusTransferenciaID = s.StatusTransferenciaID,
                LocalizacaoID = s.LocalizacaoID,
                PatrimonioID = s.PatrimonioID,
                UsuarioIDAprovacao = s.UsuarioIDAprovacao,
                UsuarioIDSolicitcao = s.UsuarioIDSolicitacao,
            }).ToList();

            return solicitacoesDto;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid id)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(id);

            if (solicitacao == null) throw new DomainException("Solicitação de transferencia não encontrada");

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                LocalizacaoID = solicitacao.LocalizacaoID,
                PatrimonioID = solicitacao.PatrimonioID,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                UsuarioIDSolicitcao = solicitacao.UsuarioIDSolicitacao,
            };

            return solicitacaoDto;
        }
    }
}
