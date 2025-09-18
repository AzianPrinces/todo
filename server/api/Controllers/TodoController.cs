using System.ComponentModel.DataAnnotations;
using api;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class TodoController(ITodoService todoService) : ControllerBase
{
    
    //GET METHOD    
    [Route(nameof(GetAllTodos))]
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAllTodos()
    {
        var todos = await todoService.GetAllTodos();
        return todos;
    }
    
    //POST METHOD
    [Route(nameof(CreateTodo))]
    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo([FromBody]CreateTodoDto dto)
    {
        var result = await todoService.CreateTodo(dto);
        return result;
    }

    [Route(nameof(ToggleChecked))]
    [HttpPut]
    public async Task<ActionResult<Todo>> ToggleChecked([FromBody] Todo t)
    {
        var result = await todoService.ToggleTodo(t);
        return result;
    }
    
}