using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.EnderecoDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _enderecoRepository.Listar();
            List<ListarEnderecoDto> enderecosDto = enderecos.Select(e => new ListarEnderecoDto
            {
                BairroID = e.BairroID,
                CEP = e.CEP,
                Complemento = e.Complemento,
                EnderecoID = e.EnderecoID,
                Logradouro = e.Logradouro,
                Numero = e.Numero
            }).ToList();

            return enderecosDto;
        }

        public ListarEnderecoDto ListarPorId(Guid id)
        {
            Endereco endereco = _enderecoRepository.BuscarPorId(id);
            ListarEnderecoDto enderecoDto = new ListarEnderecoDto
            {
                BairroID = endereco.BairroID,
                CEP = endereco.CEP,
                Complemento = endereco.Complemento,
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero
            };

            return enderecoDto;
        }

        public void Adicionar(CriarEnderecoDto endereco)
        {
            if (!_enderecoRepository.BairroExiste(endereco.BairroID)) throw new DomainException("Esse Bairro não existe");

            Endereco enderecoBanco = _enderecoRepository.BuscarPorLogradouroENumero(endereco.Logradouro, endereco.Numero, endereco.BairroID);

            if (enderecoBanco != null) throw new DomainException("Já Existe este end");

            Endereco enderecoNovo = new Endereco
            {
                CEP = endereco.CEP,
                BairroID = endereco.BairroID,
                Complemento = endereco.Complemento,
                Logradouro = endereco.Logradouro, 
                Numero = endereco.Numero,
            };

            _enderecoRepository.Adicionar(enderecoNovo);
        }

        public void Atualizar(Guid id, CriarEnderecoDto enderecoDto)
        {
            if(!_enderecoRepository.BairroExiste(enderecoDto.BairroID)) throw new DomainException("O bairro não foi encontrado");

            Endereco enderecoBanco = _enderecoRepository.BuscarPorId(id);

            if (enderecoBanco == null) throw new DomainException("Endereço não encontrado");

            enderecoBanco.Logradouro = enderecoDto.Logradouro;
            enderecoBanco.CEP = enderecoDto.CEP;
            enderecoBanco.BairroID = enderecoDto.BairroID;
            enderecoBanco.Complemento = enderecoDto.Complemento;
            enderecoBanco.Numero = enderecoDto.Numero;

            _enderecoRepository.Atualizar(enderecoBanco);
        }
    }
}
