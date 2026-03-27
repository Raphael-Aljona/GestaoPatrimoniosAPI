using GestaoPatrimonio.Applications.Rules;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.LocalizacaoDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Repositories;

namespace GestaoPatrimonio.Applications.Service
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();
            List<ListarLocalizacaoDto> localizacoesDto = localizacoes.Select(l => new ListarLocalizacaoDto
            {
                LocalizacaoId = l.LocalizacaoID,
                NomeLocal = l.NomeLocal,
                LocalSAP = l.LocalSAP,
                DescricaoSAP = l.DescricaoSAP,
                AreaId = l.AreaID,
            }).ToList();

            return localizacoesDto;
        }

        public ListarLocalizacaoDto ObterPorId(Guid id)
        {
            Localizacao localizacao = _repository.BuscarPorId(id);

            if (localizacao == null) throw new DomainException("Localização não encontrada");

            ListarLocalizacaoDto locDto = new ListarLocalizacaoDto
            {
                LocalizacaoId = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaId = localizacao.AreaID,
            };
            return locDto;
        }

        public void Adicionar(CriarLocalizacaoDto localizacaoDto)
        {
            Validar.ValidarNome(localizacaoDto.NomeLocal);

            Localizacao locBanco = _repository.BuscarPorNome(localizacaoDto.NomeLocal, localizacaoDto.AreaId);

            if (locBanco != null) throw new DomainException("Já existe uma localização com esse nome");

            if (!_repository.AreaExiste(localizacaoDto.AreaId)) throw new DomainException("Essa área não existe.");

            Localizacao localizacaoR = new Localizacao
            {
                NomeLocal = localizacaoDto.NomeLocal,
                LocalSAP = localizacaoDto.LocalSAP,
                DescricaoSAP = localizacaoDto.DescricaoSAP,
                AreaID = localizacaoDto.AreaId,
            };

            _repository.Adicionar(localizacaoR);
        }

        public void Atualizar(CriarLocalizacaoDto localizacaoDto, Guid LocalizacaoId)
        {
            Validar.ValidarNome(localizacaoDto.NomeLocal);

            Localizacao localizacaoBanco = _repository.BuscarPorId(LocalizacaoId);

            if (localizacaoBanco == null) throw new DomainException("Localização não encontrada.");

            Localizacao LocalizacaoNomeExiste = _repository.BuscarPorNome(localizacaoDto.NomeLocal, localizacaoDto.AreaId);

            if (LocalizacaoNomeExiste != null) throw new DomainException("Já existe uma localização com esse nome");

            if (!_repository.AreaExiste(localizacaoDto.AreaId)) throw new DomainException("Área não existe");

            localizacaoBanco.NomeLocal = localizacaoDto.NomeLocal;
            localizacaoBanco.LocalSAP = localizacaoDto.LocalSAP;
            localizacaoBanco.DescricaoSAP = localizacaoDto.DescricaoSAP;
            localizacaoBanco.AreaID = localizacaoDto.AreaId;

            _repository.Atualizar(localizacaoBanco);
        }
    }
}
