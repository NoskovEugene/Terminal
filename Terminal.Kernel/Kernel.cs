
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
    public IKernel Container { get; }

    private readonly ILogger<Kernel> _kernelLogger;

    public Kernel()
    {
        Container = new StandardKernel();
        ConfigureSettings(Container);
        var appSettings = Container.Get<ApplicationSettings>();
        Container.AddSerilog(configuration => configuration.WriteTo.Console(appSettings.LoggerSettings.MinimumLevel,
            appSettings.LoggerSettings.Template));
        _kernelLogger = Container.Get<ILogger<Kernel>>();
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