using System.Windows;
using CefSharp;

namespace CefSharpApp;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private BoundObject BoundObject { get; } = new ();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        DisableButtons();
        const string boundObjectName = "boundObject";
        ChromiumWebBrowser.JavascriptObjectRepository.ResolveObject += (_, eventArgs) =>
        {
            if (eventArgs.ObjectName != boundObjectName)
                return;

            eventArgs.ObjectRepository.Register(boundObjectName, BoundObject, BindingOptions.DefaultBinder);
            Dispatcher.BeginInvoke(EnableButtons);
        };
    }

    private void DisableButtons() => ChangeButtonState(false);
    private void EnableButtons() => ChangeButtonState(true);

    private void ChangeButtonState(bool isEnabled) =>
        SimpleCallButton.IsEnabled = ComplexCallButton.IsEnabled = IntertwinedCallButton.IsEnabled = isEnabled;

    private async void PerformSimpleMeasurement(object _, RoutedEventArgs __)
    {
        DisableButtons();
        var message = await BoundObject.PerformSimpleCallAsync();
        InsertLogItem(message);
        EnableButtons();
    }

    private async void PerformComplexMeasurement(object _, RoutedEventArgs __)
    {
        DisableButtons();
        var message = await BoundObject.PerformComplexCall();
        InsertLogItem(message);
        EnableButtons();
    }

    private async void PerformIntertwinedMeasurement(object sender, RoutedEventArgs e)
    {
        DisableButtons();
        var message = await BoundObject.PerformIntertwinedCall();
        InsertLogItem(message);
        EnableButtons();
    }
    
    private void InsertLogItem(string message)
    {
        var items = LogBox.Items;
        items.Insert(0, message);
        if (items.Count > 400)
            items.RemoveAt(items.Count - 1);
    }
}