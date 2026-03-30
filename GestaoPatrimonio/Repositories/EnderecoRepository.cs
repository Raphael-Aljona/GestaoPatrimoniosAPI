using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public EnderecoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            Endereco enderecoNovo = new Endereco
            {
                Bairro = endereco.Bairro,
                CEP = endereco.CEP,
                Complemento = endereco.Complemento,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
            };

            _context.SaveChanges();
        }

        public bool BairroExiste(Guid id)
        {
            return _context.Bairro.Any(e => e.BairroID == id);
        }

        public Endereco BuscarPorId(Guid id)
        {
            return _context.Endereco.Find(id);
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid id)
        {
            return _context.Endereco.FirstOrDefault(e => e.Logradouro.ToString().ToLower() == logradouro.ToLower() && e.Numero == numero && e.BairroID == id);
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.ToList();
        }
    }
}
