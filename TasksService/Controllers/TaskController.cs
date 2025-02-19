using Microsoft.AspNetCore.Mvc;
using TasksService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskDbContext _context;
    public TasksController(TaskDbContext context)
    {
        _context = context;
    }

    [HttpPost ("create")]
    [Authorize]
    public IActionResult CreateTask([FromBody] TasksService.Models.Task task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest(new { Message = "O titulo da tarefa não pode estar vazio" });
        }

        var userName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(userName))
        {
            return Unauthorized(new { Message = "Usuário não autenticado" });
        }

        try
        {
            task.CreatedBy = userName;
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
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
    [Authorize]
    public IActionResult UpdateTask(int id, TasksService.Models.Task task)
    {
        var taskToUpdate = _context.Tasks.Find(id);

        if (taskToUpdate == null)
        {
            return NotFound(new { Message = "Tarefa não encontrada" });
        }

        taskToUpdate.Status = task.Status;
        taskToUpdate.Title = task.Title;
        taskToUpdate.Description = task.Description;
        taskToUpdate.InitialTime = task.InitialTime;

        _context.SaveChanges();

        return Ok(new { Message = "Tarefa atualizada com sucesso" });
    }

    [HttpDelete("{id}")]
    [Authorize]
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
