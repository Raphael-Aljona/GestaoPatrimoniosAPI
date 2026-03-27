using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public CidadeRepository (GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(c => c.NomeCidade).ToList();
        }
        public Cidade BuscarPorId(Guid cidadeId)
        {
            return _context.Cidade.Find(cidadeId);
        }

        public void Adicionar(Cidade cidade)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Cidade cidade)
        {
            throw new NotImplementedException();
        }

        public Cidade BuscarPorNomeEstado(string nomeCidade, string estado)
        {
            throw new NotImplementedException();
        }
    }
}
