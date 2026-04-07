using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoPatrimonio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public UsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario BuscarDuplicado(string NIF, string CPF, string email, Guid? id = null)
        {
            var consulta = _context.Usuario.AsQueryable();

            if (id.HasValue)
            {
                consulta = consulta.Where(usuario => usuario.UsuarioID != id.Value);
            }

            return consulta.FirstOrDefault(u => u.NIF == NIF || u.CPF == CPF || u.Email.ToLower() == email.ToLower());
        }

        public Usuario BuscarPorId(Guid id)
        {
            return _context.Usuario.Find(id);
        }

        public bool CargoExiste(Guid cargoId)
        {
            return _context.Cargo.Any(cargo => cargo.CargoID == cargoId);
        }

        public bool EnderecoExiste(Guid enderecoId)
        {
            return _context.Endereco.Any(endereco => endereco.EnderecoID == enderecoId);
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public bool TipoUsuarioExiste(Guid tipoUsuarioId)
        {
            return _context.TipoUsuario.Any(tipoUsuario => tipoUsuario.TipoUsuarioID == tipoUsuarioId);
        }

        public void Atualizar(Usuario usuario)
        {
            if (usuario == null) return;

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if(usuarioBanco == null) return;

            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email; 
            usuarioBanco.RG = usuario.RG;
            usuarioBanco.CPF = usuario.CPF; 
            usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
            usuarioBanco.EnderecoID = usuario.EnderecoID;
            usuarioBanco.CargoID = usuario.CargoID;
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Usuario usuario)
        {
            if(usuario == null) return;

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) return;

            usuarioBanco.Ativo = usuario.Ativo;
            _context.SaveChanges();
        }

        public Usuario ObterPorNIFComTipoUsuario(string NIF)
        {
            return _context.Usuario.Include(u => u.TipoUsuario).FirstOrDefault(u => u.NIF == NIF);
        }

        public void AtualizarSenha(Usuario usuario)
        {
            if (usuario == null) return;

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) return;

            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();
        }

        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null) return;

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) return;

            usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
            _context.SaveChanges();
        }
    }
}
