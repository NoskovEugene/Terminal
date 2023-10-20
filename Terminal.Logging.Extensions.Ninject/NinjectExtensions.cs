using Ninject;
using Serilog;
using Terminal.Extensions.Ninject;

namespace Terminal.Logging.Extensions.Ninject;

public static class NinjectExtensions
{
    public static IKernel AddSerilog(this IKernel kernel, Action<LoggerConfiguration> configurationAction)
    {
        var config = new LoggerConfiguration();
        configurationAction(config);
        kernel.TryBind(typeof(ILogger<>), syntax => syntax.To(typeof(Logger<>)));
        kernel.TryBind(typeof(LoggerConfiguration), syntax => syntax.ToConstant(config));
        return kernel;
    }
}