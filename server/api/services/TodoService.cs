using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;

namespace api;

public class TodoService(MyDbContext dbContext) : ITodoService
{
    public async Task<Todo> CreateTodo(CreateTodoDto dto)
    {
        
        var myTodo = new Todo()
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.title,
            Description = dto.description,
            Isdone = false,
            Priority = dto.priority
        };
        dbContext.Todos.Add(myTodo);
        dbContext.SaveChanges();
        return myTodo;
    }

    public async Task<List<Todo>> GetAllTodos()
    {
        return dbContext.Todos.ToList();
    }
}