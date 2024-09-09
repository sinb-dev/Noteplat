using System;
using System.Data.Entity.Core.Mapping;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Noteplat.ViewModels;

namespace Noteplat;

public class ViewLocator : IDataTemplate
{
    public bool SupportsRecycling => false;

    public Control? Build(object? data)
    {
        if (data != null)
        {
            var name = data.GetType().FullName?.Replace("ViewModel", "View");
            Type? type = null;
            if (name != null)
                type = Type.GetType(name);

            if (type != null)
            {
                return (Control?)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }
        return null;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
