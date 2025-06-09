namespace AssignmentManagement.Core.Interfaces
{
    /// <summary>
    /// Interface for the logger used by the Assignment Service.
    /// 
    /// Responsible for logging debug info to the console
    /// </summary>
    public interface IAppLogger
    {
        void Log(string message);
    }
}
