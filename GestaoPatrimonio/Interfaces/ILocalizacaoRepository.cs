using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.LocalizacaoDto;

namespace GestaoPatrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
    }
}
