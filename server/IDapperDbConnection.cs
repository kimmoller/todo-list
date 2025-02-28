using System.Data;

namespace todo_list
{
    public interface IDapperDbConnection
    {
        public IDbConnection CreateConnection();
    }
}