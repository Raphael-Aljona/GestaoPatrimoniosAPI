using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public SolicitacaoTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public void Adicionar(SolicitacaoTransferencia solicaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicaoTransferencia);
            _context.SaveChanges();
        }

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public SolicitacaoTransferencia BuscarPorId(Guid id)
        {
            return _context.SolicitacaoTransferencia.Find(id);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(s => s.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null) return false;

            return _context.SolicitacaoTransferencia.Any(s => s.PatrimonioID == patrimonioId && s.StatusTransferenciaID == statusPendente.StatusTransferenciaID);
        }

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.OrderByDescending(s => s.DataCriacaoSolicitante).ToList();
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(l => l.LocalizacaoID == localizacaoId);
        }

        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId)
        {
            return _context.Usuario.Any(u => u.UsuarioID == usuarioId && u.Localizacao.Any(l => l.LocalizacaoID == localizacaoId));
        }
    }
}
