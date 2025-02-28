using Dapper;
using System.Data;

namespace todo_list
{
    public class TodoRepository(IDapperDbConnection dapperDbConnection) : ITodoRepository
    {
        public IDapperDbConnection _dapperDbConnection = dapperDbConnection;

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            using IDbConnection db = _dapperDbConnection.CreateConnection();
            return await db.QueryAsync<Todo>("SELECT * FROM Todo");
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            using IDbConnection db = _dapperDbConnection.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<Todo>("SELECT * FROM Todo WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> CreateTodoAsync(Todo Todo)
        {
            using IDbConnection db = _dapperDbConnection.CreateConnection();
            const string query = "INSERT INTO Todo (Name) VALUES (@Name); SELECT LAST_INSERT_ID();";
            return await db.ExecuteScalarAsync<int>(query, Todo);
        }

        public async Task<bool> UpdateTodoAsync(Todo Todo)
        {
            using IDbConnection db = _dapperDbConnection.CreateConnection();
            const string query = "UPDATE Todo SET Name = @Name WHERE Id = @Id";
            int rowsAffected = await db.ExecuteAsync(query, Todo);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            using IDbConnection db = _dapperDbConnection.CreateConnection();
            const string query = "DELETE FROM Todo WHERE Id = @Id";
            int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}