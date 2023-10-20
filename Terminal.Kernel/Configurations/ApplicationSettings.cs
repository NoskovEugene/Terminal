namespace Terminal.Kernel.Configurations;

public class ApplicationSettings
{
    public LoggerSettings LoggerSettings { get; }

    public ApplicationSettings(LoggerSettings loggerSettings)
    {
        LoggerSettings = loggerSettings;
    }
}