using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data;
public class AppDbContext : DbContext
{
    public DbSet<ToDo>? ToDos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
        => builder.UseSqlite("DataSource=app.db;Cache=Shared");
}