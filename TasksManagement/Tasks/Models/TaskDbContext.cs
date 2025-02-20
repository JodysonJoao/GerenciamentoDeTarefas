using Microsoft.EntityFrameworkCore;

namespace TasksManagement.Tasks.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; } = null!;
    }
}
