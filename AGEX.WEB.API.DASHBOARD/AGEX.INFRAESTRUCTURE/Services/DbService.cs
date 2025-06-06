using System.Data.SqlClient;
using System.Data;
using AGEX.CORE.Exceptions;

namespace AGEX.CORE.Interfaces.Services
{
    public class DbService : IDbService
    {
        private DataTable dt;
        private SqlConnection sql = new();
        private SqlCommand cmd = new();

        private async Task InitConnectionAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters)
        {
            dt = new DataTable();
            sql = new(cs);
            cmd = new(sp, sql)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = timeout
            };

            foreach (var item in parameters)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }
            await sql.OpenAsync();
        }

        private async Task CloseConnectionAsync()
        {
            await sql.CloseAsync();
            await cmd.DisposeAsync();
            await sql.DisposeAsync();
        }

        public async Task<DataTable> ExecSpDataAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters)
        {
            try
            {
                await InitConnectionAsync(cs, timeout, sp, parameters);
                var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
                await CloseConnectionAsync();
                await reader.CloseAsync();
                return dt;
            }
            catch (SqlException ex)
            {
                throw new CustomException(ex.Message);
            }
            finally
            {
                dt.Dispose();
                await CloseConnectionAsync();
            }
        }

        public async Task ExecSpAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters)
        {
            try
            {
                await InitConnectionAsync(cs, timeout, sp, parameters);
                await cmd.ExecuteNonQueryAsync();
                await CloseConnectionAsync();
            }
            catch (SqlException ex)
            {
                throw new CustomException(ex.Message);
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }
    }
}
