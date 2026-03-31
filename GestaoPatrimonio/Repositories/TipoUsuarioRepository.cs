using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoUsuarioRepository (GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public void Adicionar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuario.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Atualizar(TipoUsuario tipoUsuario)
        {
            if (tipoUsuario == null) throw new DomainException("Tipo do usuário não encontrado");

            TipoUsuario usuarioBanco = _context.TipoUsuario.Find(tipoUsuario.TipoUsuarioID);

            if (usuarioBanco == null) throw new DomainException("Tipo do usuário não encontrado");

            usuarioBanco.NomeTipo = tipoUsuario.NomeTipo;

            _context.SaveChanges();
        }

        public TipoUsuario BuscarPorId(Guid id)
        {
            return _context.TipoUsuario.Find(id);
        }

        public TipoUsuario BuscarPorNome(string nomeTipo)
        {
            return _context.TipoUsuario.FirstOrDefault(t => t.NomeTipo == nomeTipo);
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.ToList();
        }
    }
}
