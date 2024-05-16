using Microsoft.EntityFrameworkCore;
using ToDoList.App.Models;

namespace ToDoList.App.Data;

public sealed class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TodoItem> ToDoItems { get; set; }
}