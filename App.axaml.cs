using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AvaloniaSolarSystem;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    static IClassicDesktopStyleApplicationLifetime desktop;

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            App.desktop = desktop;
            desktop.MainWindow = new MainMenu();
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static void SetView(Window window)
    {
        desktop.MainWindow = window;
    }
}