namespace todo_list;

public record Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class TodoDB
{
    private static List<Todo> _todos =
    [
        new() {Id = 1, Name="Breakfast"},
        new() {Id = 2, Name="Coding"},
        new() {Id = 3, Name="Barber"},
        new() {Id = 4, Name="Flowers"},
    ];

    public static List<Todo> GetTodos()
    {
        return _todos;
    }

    public static Todo? GetTodo(int id)
    {
        return _todos.SingleOrDefault(todo => todo.Id == id);
    }

    public static Todo CreateTodo(Todo todo)
    {
        _todos.Add(todo);
        return todo;
    }

    public static Todo UpdateTodo(Todo update)
    {
        _todos = [.. _todos.Select(todo =>
        {
            if (todo.Id == update.Id)
            {
                todo.Name = update.Name;
            }
            return todo;
        })];
        return update;
    }

    public static void RemoveTodo(int id)
    {
        _todos = [.. _todos.FindAll(todo => todo.Id != id)];
    }
}