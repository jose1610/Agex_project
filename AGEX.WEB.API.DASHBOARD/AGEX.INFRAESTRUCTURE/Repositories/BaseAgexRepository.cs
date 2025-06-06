using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Interfaces.Services;
using AGEX.CORE.Models.Configuration;
using AGEX.INFRAESTRUCTURE.Interfaces;
using System.Data;

namespace AGEX.INFRAESTRUCTURE.Repositories
{
    public class BaseAgexRepository : IBaseAgexRepository
    {
        private readonly DbModel _dbModel;
        private readonly ICryptoService _cryptoService;
        private readonly IDbService _dbService;
        private readonly string _connectionString;

        public BaseAgexRepository(ICryptoService cryptoService, IDbService dbService, IConfigurationService configurationService, IWritableConfigurationService<ConfigurationDb> configurationWritable)
        {
            _cryptoService = cryptoService;
            _dbService = dbService;
            _dbModel = configurationService.Get<ConfigurationDb>(ConfigurationSection.ConnectionStrings).AgexDB;

            if (!_dbModel.Password.StartsWith("$"))
            {
                configurationWritable.Update(configuration =>
                {
                    configuration.AgexDB.Password = $"${_cryptoService.Encode(_dbModel.Password)}";
                });
            }
            else
                _dbModel.Password = cryptoService.Decode(_dbModel.Password);
            _connectionString = $"Server={_dbModel.Server};Database={_dbModel.Name};User Id={_dbModel.User};Password={_dbModel.Password};Encrypt=False";
        }

        public async Task ExecSpAsync(string sp, Dictionary<string, dynamic> parameters) => await _dbService.ExecSpAsync(_connectionString, _dbModel.Timeout, sp, parameters);
        public async Task<DataTable> ExecSpDataAsync(string sp, Dictionary<string, dynamic> parameters) => await _dbService.ExecSpDataAsync(_connectionString, _dbModel.Timeout, sp, parameters);
    }
}
