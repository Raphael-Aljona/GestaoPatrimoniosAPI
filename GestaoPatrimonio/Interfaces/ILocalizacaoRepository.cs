using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao BuscarPorId(Guid localizacaoId);
        void Adicionar(Localizacao localizacao);
        Localizacao BuscarPorNome(string nome);
        bool AreaExiste(Guid areaId);
        void Atualizar(Localizacao localizacao);
    }
}
