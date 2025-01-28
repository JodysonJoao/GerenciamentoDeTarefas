using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly AuthDbContext _context;

    public UsersController(AuthDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
        {
            return BadRequest("O nome de usuario não pode estar vazio");
        }

        if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Email e senha são obrigatorios");
        }

        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new { Message = "Usuario criado com sucesso" });
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Users_Email"))
            {
                return Conflict(new
                {
                    Message = "Esse email já está em uso, use outro email"
                });
            }

            return StatusCode(500, new
            {
                Message = "Ocorreu um erro ao salvar os dados",
                ErrorDetails = ex.Message
            });
        }
    }

}