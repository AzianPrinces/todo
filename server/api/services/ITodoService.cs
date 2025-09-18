using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api;

public interface ITodoService
{
    Task<Todo> CreateTodo(CreateTodoDto dto);
    Task<List<Todo>> GetAllTodos();
    Task<Todo> ToggleTodo(Todo todo);
}