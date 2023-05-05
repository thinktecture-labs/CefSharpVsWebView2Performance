namespace WebView2App;

public static class TimeFormatter
{
    // According to the Stopwatch implementation, a tick represents 1/10,000 of a millisecond,
    // i.e. 100ns.
    public static string FormatElapsedTicks(this long elapsedTicks)
    {
        var intervalInNanoseconds = elapsedTicks * 100; 
        return elapsedTicks switch
        {
            < 1_000 => $"{intervalInNanoseconds}ns",
            < 1_000_000 => $"{elapsedTicks / 1000.0:N3}µs",
            _ => $"{elapsedTicks / 1_000_000.0:N3}ms"
        };
    }
}