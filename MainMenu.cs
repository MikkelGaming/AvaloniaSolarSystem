
using Avalonia.Controls;

namespace AvaloniaSolarSystem;

public class MainMenu : Window
{
    public MainMenu()
    {

        StackPanel stackPanel = new()
        {
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
        };

        Content = stackPanel;

        Button singleButton = new()
        {
            Content = "Single threaded"
        };
        singleButton.Click += (_, _) =>
        {
            //App.SetView(new MainWindow());
            Content = new MainWindow();
        };

        stackPanel.Children.Add(singleButton);
    }
}