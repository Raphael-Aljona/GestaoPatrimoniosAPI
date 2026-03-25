using GestaoPatrimonio.Exceptions;

namespace GestaoPatrimonio.Applications.Rules
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Esse campo é obrigatório");
            }
        }
    }
}
