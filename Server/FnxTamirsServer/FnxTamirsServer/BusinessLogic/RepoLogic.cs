using FnxTamirsServer.Models;
using FnxTamirsServer.Services;
using Microsoft.OpenApi.Services;

namespace FnxTamirsServer.BusinessLogic
{
  public class RepoLogic(ISessionService sessionService  , IGitHubRepoService gitHubRepoService ) :IRepoLogic
  {
    public async Task<GitHubSearchResponse> GetGitHubRepoAsync(string repoName)
    {
      var repos = await gitHubRepoService?.GetGitHubRepoAsync(repoName);
      var bookmarks = sessionService.GetBookmarks()?.Result;

      if (repos?.Items?.Count > 0)
      {
        if (bookmarks?.Count() > 0)
        {
          foreach (var repo in repos.Items)
          {
            repo.Bookmarked = bookmarks?.Where(b => repo.Id == b.Id)?.Count() > 0;
          }
        }
      }
      return repos;
    }
  }
}
