using System.Text.Json.Serialization;

namespace TasksManagement.Auth.Models
{
    public class GitHubUser
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
