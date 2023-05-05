using System.Collections.Generic;

namespace WebView2App;

public sealed class WebMessage
{
    public required string Type { get; init; }
    public int SimpleResult { get; init; }
    public List<SampleDto>? ComplexResult { get; init; }
}