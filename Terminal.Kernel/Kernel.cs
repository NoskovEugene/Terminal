
using Ninject;
using Serilog;

using Terminal.Files.Ini;
using Terminal.Files.Ini.Extensions;
using Terminal.Kernel.Configurations;
using Terminal.Kernel.Extensions;
using Terminal.Logging;
using Terminal.Logging.Extensions.Ninject;

namespace Terminal.Kernel;

public class Kernel
{
    public IKernel MainContainer { get; }

    private readonly ILogger<Kernel> _kernelLogger;

    public Kernel()
    {
        MainContainer = new StandardKernel();
        ConfigureSettings(MainContainer);
        var appSettings = MainContainer.Get<ApplicationSettings>();
        MainContainer.AddSerilog(configuration => configuration.WriteTo.Console(appSettings.LoggerSettings.MinimumLevel,
            appSettings.LoggerSettings.Template));
        _kernelLogger = MainContainer.Get<ILogger<Kernel>>();
        _kernelLogger.Information("Kernel initialized");
    }

    private void ConfigureSettings(IKernel container)
    {
        var file = new IniFile().Load("settings.ini");
        container.Bind<IniFile>().ToConstant(file);
        container.Configure<LoggerSettings>();
        container.Bind<ApplicationSettings>().To<ApplicationSettings>().InSingletonScope();
    }
}