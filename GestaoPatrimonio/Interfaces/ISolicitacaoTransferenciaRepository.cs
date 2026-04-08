using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorId(Guid id);
        bool ExisteSolicitacaoPendente(Guid patrimonioId);
        bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicaoTransferencia);
        bool LocalizacaoExiste(Guid localizacaoId);
        Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);
    }
}
