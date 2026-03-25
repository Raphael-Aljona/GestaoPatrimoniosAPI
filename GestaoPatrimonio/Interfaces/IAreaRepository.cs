using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();
        Area BuscarPorId(Guid id);
        Area BuscarPorNome(string nomeArea);
        void Adicionar(Area area);
        void Atualizar(Area area);
    }
}
