using ToDoList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// In-memory list acting as a fake database
var todoItems = new List<Item>
{
    new Item { Id = 1, Title = "Learn ASP.NET Core", IsComplete = false },
    new Item { Id = 2, Title = "Build Minimal API", IsComplete = true },
};

// Get All
app.MapGet("/api/todos", () => Results.Ok(todoItems));

// Get by Id
app.MapGet("/api/todos/{id}", (int id) =>
{
    var todo = todoItems.FirstOrDefault(x => x.Id == id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

// Create
app.MapPost("/api/todos", (Item newItem) =>
{
    newItem.Id = todoItems.Count > 0 ? todoItems.Max(x => x.Id) + 1 : 1;
    todoItems.Add(newItem);
    return Results.Created($"/api/todos/{newItem.Id}", newItem);
});

// Update
app.MapPut("/api/todos/{id}", (int id, Item updatedItem) =>
{
    var todo = todoItems.FirstOrDefault(x => x.Id == id);
    if (todo is null)
    {
        return Results.NotFound();
    }

    todo.Title = updatedItem.Title;
    todo.IsComplete = updatedItem.IsComplete;
    return Results.Ok(todo);
});

// Delete
app.MapDelete("/api/todos/{id}", (int id) =>
{
    var todo = todoItems.FirstOrDefault(x => x.Id == id);
    if (todo is null)
    {
        return Results.NotFound();
    }

    todoItems.Remove(todo);
    return Results.NoContent();
});

app.Run();
