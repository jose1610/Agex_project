namespace AGEX.CORE.Interfaces.Services
{
    public interface IParseService
    {
        T Deserealize<T>(dynamic model);
        string Serialize(dynamic model);
    }
}