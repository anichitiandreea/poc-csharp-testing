using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApi.Data;
using RestApi.Services;
using RestApi.Services.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql("Server=192.168.0.73;Port=5432;Database=UnitTesting;User Id=postgres;Password=parola;"));

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IUserService, UserService>();

WebApplication app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
