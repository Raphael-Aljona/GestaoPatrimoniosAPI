using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.CidadeDto;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar();
            List<ListarCidadeDto> cidadesDto = cidades.Select(c => new ListarCidadeDto
            {
                CidadeID = c.CidadeID,
                Estado = c.Estado,
                NomeCidade = c.NomeCidade
            }).ToList();

            return cidadesDto;
        }
    }
}
