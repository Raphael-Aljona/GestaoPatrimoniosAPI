using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null) return;

            StatusPatrimonio statusPatrimonioExistente = _context.StatusPatrimonio.Find(statusPatrimonio.StatusPatrimonioID);

            if (statusPatrimonioExistente == null) return;

            statusPatrimonioExistente.NomeStatus = statusPatrimonio.NomeStatus;

            _context.SaveChanges();
        }

        public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Find(statusPatrimonioId);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.NomeStatus == nomeStatus);
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.ToList();    
        }
    }
}
