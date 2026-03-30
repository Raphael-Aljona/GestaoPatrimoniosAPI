using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();
        Endereco BuscarPorId(Guid id);
        void Adicionar(Endereco endereco);
        void Atualizar(Endereco endereco);
        Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid BairroId);
        bool BairroExiste(Guid bairroId);
    }
}
