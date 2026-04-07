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

        public static void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new DomainException("Estado é obrigatório.");
            }
        }

        public static void ValidarNIF(string nif)
        {
            if (string.IsNullOrWhiteSpace(nif)) throw new DomainException("NIF é obrigatório");
        }

        public static void ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) throw new DomainException("CPF é obrigatório");
        }

        public static void ValidarEmail(string email)
        {
            if(string.IsNullOrEmpty(email)) throw new DomainException("Email é obrigatório");
        }

        public static void ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha)) throw new DomainException("Senha é obrigatória");
        }
    }
}
