
using System.Data;

namespace AGEX.CORE.Interfaces.Repositories
{
    public interface IBaseAgexRepository
    {
        Task ExecSpAsync(string sp, Dictionary<string, dynamic> parameters);
        Task<DataTable> ExecSpDataAsync(string sp, Dictionary<string, dynamic> parameters);
    }
}