using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario BuscarPorId(Guid id);
        Usuario BuscarDuplicado(string NIF, string CPF, string email, Guid? id = null);
        bool EnderecoExiste(Guid enderecoId);
        bool CargoExiste(Guid cargoId);
        bool TipoUsuarioExiste(Guid tipoUsuarioId);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void AtualizarStatus(Usuario usuario);
        Usuario ObterPorNIFComTipoUsuario(string NIF);
        void AtualizarSenha(Usuario usuario);
        void AtualizarPrimeiroAcesso(Usuario usuario);
    }
}
