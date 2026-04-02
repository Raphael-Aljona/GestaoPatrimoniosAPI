using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoPatrimonioRepository (GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if (tipoPatrimonio == null) return;

            TipoPatrimonio tipoPatrimonioExistente = _context.TipoPatrimonio.Find(tipoPatrimonio.TipoPatrimonioID);

            if (tipoPatrimonioExistente == null) return;

            tipoPatrimonioExistente.NomeTipo = tipoPatrimonio.NomeTipo;

            _context.SaveChanges();
        }

        public TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.Find(tipoPatrimonioId);
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(t => t.NomeTipo.ToLower() == nomeTipo);
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.ToList();
        }
    }
}
