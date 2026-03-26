using GestaoPatrimonio.Applications.Rules;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.AreaDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Service
{
    public class AreaService
    {
        public readonly IAreaRepository _repository;

        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarAreaDto> Listar()
        {
            List<Area> areas = _repository.Listar();

            List<ListarAreaDto> areasDto = areas.Select(area => new ListarAreaDto
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            }).ToList();

            return areasDto;
        }

        public ListarAreaDto ListarPorId(Guid id)
        {
            Area area = _repository.BuscarPorId(id);

            if (area == null) throw new DomainException("Área não encontrada");

            ListarAreaDto areaDto = new ListarAreaDto
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            };

            return areaDto;
        }

        public void Adicionar(CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaBanco = _repository.BuscarPorNome(dto.NomeArea);

            if (areaBanco != null) throw new DomainException("Já existe uma área cadastrada com esse nome.");

            Area area = new Area
            {
                NomeArea = dto.NomeArea,
                // Não estamos utilizando o método abaixo para gerar IDs, pois foi setado no SSMS
                // AreaID = Guid.NewGuid(),
            };

            _repository.Adicionar(area);
        }

        public void Atualizar(Guid id, CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaBanco = _repository.BuscarPorId(id);
            if (areaBanco == null) throw new DomainException("Área não encontrada");

            Area area = _repository.BuscarPorNome(dto.NomeArea);

            if (area != null) throw new DomainException("Já existe uma área cadastrada com esse nome");

            areaBanco.NomeArea = dto.NomeArea;
            _repository.Atualizar(areaBanco);
        }
    }
}
