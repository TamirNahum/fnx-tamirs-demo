
using FnxTamirsServer.BusinessLogic;
using FnxTamirsServer.Models;
using FnxTamirsServer.Services;
using FnxTamirsServer.Services.Implementaions;
using FnxTamirsServer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:4200",
                                            "http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
                    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
    });

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddScoped<IGitHubRepoService, GitHubRepoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddScoped<IRepoLogic, RepoLogic>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapPost("/login", async (LoginRequest user, IAuthService service) =>
{
  var token = await service.GenerateToken(user.Username);
  return new LoginResponse()
  {
    Token = token,
    Username = user.Username
  };
});

app.MapGet("/searchrepo", async (string repoName, IRepoLogic repoLogic) =>
{
  return await repoLogic.GetGitHubRepoAsync(repoName);
}).RequireAuthorization();

app.MapGet("/bookmarks", async (ISessionService service) =>
{
  return await service.GetBookmarks();
}).RequireAuthorization();

app.MapPost("/bookmark", async (GitHubRepository repo, ISessionService service) =>
{
  return  service.AddBookmark(repo);
}).RequireAuthorization();

app.MapDelete("/bookmark", async (long id, ISessionService service) =>
{
  return service.RemoveBookmark(id);
}).RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
