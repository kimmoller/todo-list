namespace todo_list
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task<int> CreateTodoAsync(Todo Todo);
        Task<bool> UpdateTodoAsync(Todo Todo);
        Task<bool> DeleteTodoAsync(int id);
    }
}