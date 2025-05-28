namespace FnxTamirsServer.Services.Interfaces
{
  public interface IAuthService
  {
    public Task<string> GenerateToken(string username);
  }
}
