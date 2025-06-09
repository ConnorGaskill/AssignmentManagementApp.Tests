using AssignmentManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Services
{
    /// <summary>
    /// Object representing the logger used by the Assignment Service.
    /// 
    /// Responsible for logging debug info to the console
    /// </summary>
    public class ConsoleAppLogger : IAppLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}
