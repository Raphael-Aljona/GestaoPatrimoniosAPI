using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.LocalizacaoDto;
using GestaoPatrimonio.Exceptions;
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

        public Localizacao BuscarPorId(Guid localizacaoId)
        {
            return _context.Localizacao.Find(localizacaoId);
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao.OrderBy(l => l.NomeLocal).ToList();
        }

        public bool AreaExiste(Guid areaId)
        {
            return _context.Area.Any(a => a.AreaID == areaId);
        }

        public void Adicionar(Localizacao localizacao)
        {
            _context.Localizacao.Add(localizacao);
            _context.SaveChanges();
        }

        public Localizacao BuscarPorNome(string nome, Guid areaId)
        {
            return _context.Localizacao.FirstOrDefault(l => l.NomeLocal.ToLower() == nome.ToLower() && l.AreaID == areaId);
        }

        public void Atualizar(Localizacao localizacao)
        {
            if (localizacao == null) return;

            Localizacao localizacaoBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

            if (localizacaoBanco == null) return;

            localizacaoBanco.NomeLocal = localizacao.NomeLocal;
            localizacaoBanco.LocalSAP = localizacao.LocalSAP;
            localizacaoBanco.DescricaoSAP = localizacao.DescricaoSAP;
            localizacaoBanco.AreaID = localizacao.AreaID;

            _context.SaveChanges();
        }
    }
}
