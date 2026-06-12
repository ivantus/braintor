using web.api.Models;
using web.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace web.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public ActionResult<List<Todo>> GetAll()
    {
        return Ok(_todoService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
            return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<Todo> Create([FromBody] Todo todo)
    {
        if (string.IsNullOrWhiteSpace(todo.Title))
            return BadRequest("Title is required");

        var created = _todoService.Add(todo);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] Todo todo)
    {
        if (string.IsNullOrWhiteSpace(todo.Title))
            return BadRequest("Title is required");

        var success = _todoService.Update(id, todo);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var success = _todoService.Delete(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
