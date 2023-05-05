using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace WebView2App;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private Stopwatch Stopwatch { get; } = new ();
    private JsonSerializerOptions JsonOptions { get; } =
        new () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        DisableButtons();
        await WebView2.EnsureCoreWebView2Async();
        WebView2.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
        EnabledButtons();
    }

    private void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        var webMessage = JsonSerializer.Deserialize<WebMessage>(e.WebMessageAsJson, JsonOptions);
        Stopwatch.Stop();
        EnabledButtons();
        if (webMessage is null)
            throw new InvalidDataException("Expected web message");
        if (webMessage.Type == "simple")
        {
            if (webMessage.SimpleResult != 42)
                throw new InvalidDataException("Expected 42 as simple result");

            InsertLogItem($"Simple call took {Stopwatch.ElapsedTicks.FormatElapsedTicks()}");
        }
        else if (webMessage.Type == "complex")
        {
            if (webMessage.ComplexResult is not { Count: 100 })
                throw new InvalidDataException("Expected 100 items as complex result");

            InsertLogItem($"Complex call took {Stopwatch.ElapsedTicks.FormatElapsedTicks()}");
        }
        else
        {
            throw new InvalidDataException("Expected type \"simple\" or \"complex\"");
        }
    }

    private void EnabledButtons() => SetButtonState(true);
    private void DisableButtons() => SetButtonState(false);

    private void SetButtonState(bool isEnabled) =>
        ComplexCallButton.IsEnabled = SimpleCallButton.IsEnabled = isEnabled;

    private void PerformSimpleMeasurement(object _, RoutedEventArgs __) => PerformMeasurement("simple");
    private void PerformComplexMeasurement(object _, RoutedEventArgs __) => PerformMeasurement("complex");

    private void PerformMeasurement(string type)
    {
        DisableButtons();
        Stopwatch.Restart();
        WebView2.CoreWebView2.PostWebMessageAsString(type);
    }

    private void InsertLogItem(string message)
    {
        var items = LogBox.Items;
        items.Insert(0, message);
        if (items.Count > 400)
            items.RemoveAt(items.Count - 1);
    }
}