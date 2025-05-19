using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Services;

namespace AssignmentManagement.API;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IAppLogger, ConsoleAppLogger>();
        builder.Services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
        builder.Services.AddSingleton<IAssignmentService, AssignmentService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}