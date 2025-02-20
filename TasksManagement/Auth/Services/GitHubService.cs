using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TasksManagement.Auth.Models;

namespace TasksManagement.Auth.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        }

        public async Task<GitHubUser> GetGitHubName(string GitHubUsername)
        {
            var response = await _httpClient.GetAsync($"https://api.github.com/users/{GitHubUsername}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GitHubUser>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
