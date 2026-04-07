using System.Security.Cryptography;
using System.Text;

namespace GestaoPatrimonio.Applications.Authentication
{
    public class CriptografiaUsuario
    {
        public static byte[]CriptografarSenha(string senha)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
            byte[] senhaCriptograda = sha256.ComputeHash(bytesSenha);   

            return senhaCriptograda;    
        }
    }
}
