using Microsoft.AspNetCore.Mvc;
using TasksManagement.Auth.Services;

namespace TasksManagement.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly GitHubService _gitHubService;

        public AuthController(JwtService jwtService, GitHubService gitHubService)
        {
            _jwtService = jwtService;
            _gitHubService = gitHubService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.GitHubUsername))
            {
                return BadRequest(new { message = "O campo GitHubUsername é obrigatório." });
            }
            var userData = await _gitHubService.GetGitHubName(request.GitHubUsername);

            if (userData == null)
            {
                return NotFound(new { message = "Usuário do GitHub não encontrado." });
            }

            var token = _jwtService.GenerateToken(userData.Login, userData.Name);

            return Ok(new
            {
                Token = token,
                userData.Name,
                Username = userData.Login
            });
        }
    }

    public class LoginRequest
    {
        public string GitHubUsername { get; set; }
    }
}
