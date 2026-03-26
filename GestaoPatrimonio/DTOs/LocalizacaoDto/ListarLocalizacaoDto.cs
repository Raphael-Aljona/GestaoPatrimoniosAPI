namespace GestaoPatrimonio.DTOs.LocalizacaoDto
{
    public class ListarLocalizacaoDto
    {
        public string NomeLocal { get; set; } = string.Empty;
        public int LocalSAP { get; set; }
        public string DescricaoSAP { get; set; }
        public Guid AreaId { get; set; } 
        public Guid LocalizacaoId { get; set; }
    }
}
