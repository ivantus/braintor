using web.api.Models;

namespace web.api.Services;

public interface ITodoService
{
    List<Todo> GetAll();
    Todo? GetById(int id);
    Todo Add(Todo todo);
    bool Update(int id, Todo todo);
    bool Delete(int id);
}

public class TodoService : ITodoService
{
    private static List<Todo> _todos = new()
    {
        new Todo { Id = 1, Title = "Learn Angular", IsCompleted = false, CreatedAt = DateTime.UtcNow },
        new Todo { Id = 2, Title = "Build Todo App", IsCompleted = false, CreatedAt = DateTime.UtcNow },
        new Todo { Id = 3, Title = "Learn C#", IsCompleted = true, CreatedAt = DateTime.UtcNow.AddDays(-1) }
    };

    private static int _nextId = 4;

    public List<Todo> GetAll() => _todos;

    public Todo? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);

    public Todo Add(Todo todo)
    {
        todo.Id = _nextId++;
        todo.CreatedAt = DateTime.UtcNow;
        _todos.Add(todo);
        return todo;
    }

    public bool Update(int id, Todo todo)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        existing.Title = todo.Title;
        existing.IsCompleted = todo.IsCompleted;
        return true;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo == null) return false;

        _todos.Remove(todo);
        return true;
    }
}
