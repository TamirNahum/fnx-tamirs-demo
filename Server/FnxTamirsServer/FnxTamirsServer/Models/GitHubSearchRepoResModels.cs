
using System.Buffers;
using System.Text.Json.Serialization;

namespace FnxTamirsServer.Models
{
  public class GitHubSearchResponse
  {
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("items")]
    public List<GitHubRepository> Items { get; set; }
  }

  public class GitHubRepository
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("owner")]
    public GitHubUser Owner { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
    public bool Bookmarked { get; set; } = false;

  }

  public class GitHubUser
  {
    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }
  }
}
