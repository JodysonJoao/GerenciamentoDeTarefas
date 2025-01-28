using Microsoft.AspNetCore.Mvc;
using TasksService.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskDbContext _context;
    public TaskController(TaskDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateTask(TasksService.Models.Task task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest("O titulo da tarefa não pode estar vazio");
        }

        try
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(new { Message = "Tarefa criada com sucesso" });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new
            {
                Message = "Ocorreu um erro ao salvar os dados",
                ErrorDetails = ex.InnerException?.Message ?? ex.Message
            });
        }
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(_context.Tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetTask(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound(new { Message = "Tarefa não encontrada" });
        }

        return Ok(task);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TasksService.Models.Task task)
    {
        var taskToUpdate = _context.Tasks.Find(id);

        if (taskToUpdate == null)
        {
            return NotFound(new { Message = "Tarefa não encontrada" });
        }

        taskToUpdate.Status = task.Status;

        _context.SaveChanges();

        return Ok(new { Message = "Tarefa atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound(new { Message = "Tarefa não encontrada" });
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return Ok(new { Message = "Tarefa deletada com sucesso" });
    }
}
