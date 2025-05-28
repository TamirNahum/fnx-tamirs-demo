using FnxTamirsServer.Models;

namespace FnxTamirsServer.BusinessLogic
{
  public interface IRepoLogic
  {
    public Task<GitHubSearchResponse> GetGitHubRepoAsync(string repoName);
  }
}
