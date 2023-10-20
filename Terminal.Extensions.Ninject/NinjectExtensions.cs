using Ninject;
using Ninject.Syntax;

namespace Terminal.Extensions.Ninject;

public static class NinjectExtensions
{
    public static void TryBind<T>(this IKernel kernel, Action<IBindingToSyntax<T>> bindingAction)
    {
        if (!kernel.GetBindings(typeof(T)).Any())
        {
            bindingAction(kernel.Bind<T>());
        }
    }

    public static void TryBind(this IKernel kernel, Type type, Action<IBindingToSyntax<object>> bindingAction)
    {
        if (!kernel.GetBindings(type).Any())
        {
            bindingAction(kernel.Bind(type));
        }
    }
}