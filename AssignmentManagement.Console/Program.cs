using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Services;
using AssignmentManagement.UI;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace AssignmentManagement.Console
{
    public class Program
    {
        private static void Main(string[] args) // 🔥 Static Main method — this is required
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAppLogger, ConsoleAppLogger>();
            services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
            services.AddSingleton<IAssignmentService, AssignmentService>();
            services.AddSingleton<ConsoleUI>();

            var serviceProvider = services.BuildServiceProvider();
            var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();

            consoleUI.Run();
        }
    }
}
public partial class Program { };