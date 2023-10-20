using System.Security.Cryptography.X509Certificates;
using Ninject;
using Terminal.Extensions.Ninject;
using Terminal.Files.Ini;
using Terminal.Files.Ini.Extensions;

namespace Terminal.Kernel.Extensions;

public static class NinjectExtensions
{
    public static IKernel Configure<T>(this IKernel kernel)
        where T: new()
    {
        return Configure<T>(kernel, typeof(T).Name);
    }

    public static IKernel Configure<T>(this IKernel kernel, string sectionName)
        where T: new()
    {
        var file = kernel.Get<IniFile>();
        var obj = file.Get<T>(sectionName);
        kernel.TryBind(typeof(T), syntax => syntax.ToConstant(obj));
        return kernel;
    }
}