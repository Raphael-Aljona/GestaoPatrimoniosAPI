using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.LocalizacaoDto;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public LocalizacaoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao.OrderBy(l => l.NomeLocal).ToList();
        }


    }
}
