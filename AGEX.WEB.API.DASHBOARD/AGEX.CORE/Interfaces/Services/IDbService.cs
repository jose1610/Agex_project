using System.Data;

namespace AGEX.CORE.Interfaces.Services
{
    public interface IDbService
    {
        Task ExecSpAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters);
        Task<DataTable> ExecSpDataAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters);
    }
}