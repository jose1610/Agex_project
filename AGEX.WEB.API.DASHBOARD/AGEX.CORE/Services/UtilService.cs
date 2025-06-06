namespace AGEX.CORE.Services
{
    public class UtilService
    {
        public static string GetId(int len) => Guid.NewGuid().ToString().Replace("-", "")[..len];
    }
}
