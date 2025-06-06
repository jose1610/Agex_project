namespace AGEX.CORE.Interfaces.Services
{
    public interface ICryptoService
    {
        string Decode(string data);
        string Encode(string data);
    }
}