using System.Windows;

namespace WebView2App;

public sealed partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindow = new MainWindow();
        MainWindow.Show();
    }
}