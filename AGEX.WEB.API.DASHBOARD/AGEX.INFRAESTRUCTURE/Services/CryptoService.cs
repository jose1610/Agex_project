using AGEX.CORE.Interfaces.Services;
using System.Text;
using vng_crypto;

namespace AGEX.INFRAESTRUCTURE.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly Crypt encrypt;

        public CryptoService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encrypt = new Crypt();
        }

        public string Encode(string data)
        {
            return encrypt.CryptData(data, Crypt.Encriptacion.Encripta).ToString();
        }

        public string Decode(string data)
        {
            return encrypt.CryptData($"{data}", Crypt.Encriptacion.Desencripta).ToString();
        }
    }
}
