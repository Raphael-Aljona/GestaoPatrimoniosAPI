namespace GestaoPatrimonio.DTOs.SolicitacaoTransferenciaDto
{
    public class ListarSolicitacaoTransferenciaDto
    {
        public Guid TransferenciaID { get; set; }
        public DateTime DataCriacaoSolicitante { get; set; }
        public DateTime? DataResposta {  get; set; }
        public string Justificativa { get; set; } = string.Empty;
        public Guid StatusTransferenciaID { get; set; }
        public Guid? UsuarioIDAprovacao { get; set; }
        public Guid UsuarioIDSolicitcao { get; set; }
        public Guid PatrimonioID {  get; set; }
        public Guid LocalizacaoID { get; set; }
    }
}
