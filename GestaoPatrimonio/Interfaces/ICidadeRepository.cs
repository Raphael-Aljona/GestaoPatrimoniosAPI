using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ICidadeRepository
    {
        List<Cidade> Listar();
        Cidade BuscarPorId(Guid cidadeId);
        Cidade BuscarPorNomeEstado(string nomeCidade, string estado);
        void Adicionar(Cidade cidade);
        void Atualizar(Cidade cidade);
    }
}
