using System.Text.Json.Serialization;

namespace GitHubUsers.Models
{
    public class GitHubUserDto
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
