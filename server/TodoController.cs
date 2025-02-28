using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace todo_list;

[Route("todos")]
[ApiController]
public class TodoController(ITodoRepository todoRepository) : ControllerBase
{
    private readonly ITodoRepository _repository = todoRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        var todos = await _repository.GetAllTodosAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(int id)
    {
        var todo = await _repository.GetTodoByIdAsync(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
    {
        int newTodoId = await _repository.CreateTodoAsync(todo);
        todo.Id = newTodoId;
        return CreatedAtAction(nameof(GetTodo), new { id = newTodoId }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, Todo todo)
    {
        if (id != todo.Id)
        {
            return BadRequest();
        }

        bool updated = await _repository.UpdateTodoAsync(todo);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        bool deleted = await _repository.DeleteTodoAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}