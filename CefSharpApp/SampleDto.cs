namespace CefSharpApp;

// ReSharper disable once ClassNeverInstantiated.Global -- instantiated by CefSharp serializer
public sealed class SampleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int X { get; set; }
    public double Y { get; set; }
}