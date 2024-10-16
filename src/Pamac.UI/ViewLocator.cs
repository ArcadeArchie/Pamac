using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

using Microsoft.Extensions.DependencyInjection;

using Pamac.UI.ViewModels;

namespace Pamac.UI;

public static class ViewLocatorHelpers
{
    public static IServiceCollection AddView<TView, TViewModel>(this IServiceCollection services)
        where TView : Control, new()
        where TViewModel : ViewModelBase =>
        services.AddSingleton(new ViewLocator.ViewLocationDescriptor(typeof(TViewModel), () => new TView()));
}

public class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control>> _views;

    public ViewLocator(IEnumerable<ViewLocationDescriptor> descriptors)
    {
        _views = descriptors.ToDictionary(x => x.ViewModel, x => x.Factory);
    }

    public Control? Build(object? data) => data is null ? null : _views[data.GetType()]();
    public bool Match(object? param) => param is not null && _views.ContainsKey(param.GetType());

    public record ViewLocationDescriptor(Type ViewModel, Func<Control> Factory);
}