using System.Data;
using MySqlConnector;

namespace todo_list
{
    public class DapperDbConnection : IDapperDbConnection
    {
        public readonly string _connectionString;

        public DapperDbConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default"!);
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

    }
}