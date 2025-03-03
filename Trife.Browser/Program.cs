using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using System;

namespace Trife.Browser;

class Program
{
    [STAThread]
    private static void Main(string[] args) => BuildAvaloniaApp()
            .WithInterFont()
            .UseReactiveUI()
            .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}
