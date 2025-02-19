using Microsoft.AspNetCore.Mvc;
using GitHubUsers.Services;
using System.Threading.Tasks;

namespace GitHubUsers.Controllers
{
    [ApiController]
    [Route("api/github")]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpPost("user")]
        public async Task<IActionResult> GetGitHubUser([FromBody] GitHubUserRequest request)
        {
            if (string.IsNullOrEmpty(request.GitHubUsername))
            {
                return BadRequest(new { message = "The GitHubUsername field is required." });
            }

            var result = await _gitHubService.GetGitHubName(request.GitHubUsername);
            return Ok(result);
        }
    }

    public class GitHubUserRequest
    {
        public string GitHubUsername { get; set; }
    }
}
