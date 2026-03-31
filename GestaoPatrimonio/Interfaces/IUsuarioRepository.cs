using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
    }
}
