namespace ToDoApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult GetAllToDo([FromServices] AppDbContext context)
        => Ok(context.ToDos!.ToList());
    [HttpGet("/{id:int}")]
    public IActionResult GetToDo([FromServices] AppDbContext context, [FromRoute] int id)
        => Ok(context.ToDos!.FirstOrDefault(x => x.Id == id)!);

    [HttpPost("/")]
    public IActionResult Post([FromServices] AppDbContext context, [FromBody] ToDo todo)
    {
        context.ToDos!.Add(todo);
        context.SaveChanges();
        return Created($"/{todo.Id}", todo);
    }
    [HttpPut("/{id:int}")]
    public IActionResult Update([FromServices] AppDbContext context, [FromBody] ToDo todo, [FromRoute] int id)
    {
        ToDo t = context.ToDos!.FirstOrDefault(x => x.Id == id)!;
        if (t != null)
        {
            t.Done = todo.Done;
            t.Title = todo.Title;
            context.ToDos!.Update(t);
            context.SaveChanges();
            return Ok(todo);
        }
        return NotFound();
    }
    [HttpDelete("/{id:int}")]
    public IActionResult Delete([FromServices] AppDbContext context, [FromRoute] int id)
    {
        ToDo t = context.ToDos!.FirstOrDefault(x => x.Id == id)!;
        if (t != null)
        {
            context.ToDos!.Remove(t);
            context.SaveChanges();
            return Ok(t);
        }
        return NotFound();
    }
}