using Microsoft.EntityFrameworkCore;

namespace TasksService.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; } = null!;
    }
}
