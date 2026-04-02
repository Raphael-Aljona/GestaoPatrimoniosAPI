namespace GestaoPatrimonio.DTOs.StatusPatrimonioDto
{
    public class ListarStatusPatrimonio
    {
        public Guid StatusPatrimonioID { get; set; }

        public string NomeStatus { get; set; } = string.Empty;
    }
}
