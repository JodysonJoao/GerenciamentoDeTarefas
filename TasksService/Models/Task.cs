namespace TasksService.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Pendente";

        public DateTime? InitialTime { get; set; } = DateTime.UtcNow;
    }
}
