namespace Library.Logging.Service;

public interface ILoggerService
{
    void Debug(string message);
    void Info(string message);
    void Success(string message);
    void Warning(string message);
    void Error(string message);
}