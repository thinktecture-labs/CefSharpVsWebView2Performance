using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CefSharp;

namespace CefSharpApp;

public sealed class BoundObject : IDisposable
{

    private Stopwatch Stopwatch { get; } = new ();
    private IJavascriptCallback? SimpleCallback { get; set; }
    private IJavascriptCallback? ComplexCallback { get; set; }
    private IJavascriptCallback? IntertwinedCallback { get; set; }
    private TaskCompletionSource<string>? IntertwinedCompletionSource { get; set; }

    public void Dispose()
    {
        SimpleCallback?.Dispose();
        ComplexCallback?.Dispose();
        IntertwinedCallback?.Dispose();
    }

    // ReSharper disable once UnusedMember.Global -- called by Angular
    public void SetUp(IJavascriptCallback simpleCallback,
                      IJavascriptCallback complexCallback,
                      IJavascriptCallback intertwinedCallback)
    {
        SimpleCallback = simpleCallback;
        ComplexCallback = complexCallback;
        IntertwinedCallback = intertwinedCallback;
    }

    public async Task<string> PerformSimpleCallAsync()
    {
        Stopwatch.Restart();
        var response = await SimpleCallback!.ExecuteAsync();
        Stopwatch.Stop();
        if ((int) response.Result != 42)
            throw new InvalidDataException("Expected 42 from simple callback");
        return $"Simple call took {Stopwatch.ElapsedTicks.FormatElapsedTicks()}";
    }

    public async Task<string> PerformComplexCall()
    {
        Stopwatch.Restart();
        var response = await ComplexCallback!.ExecuteAsync();
        Stopwatch.Stop();
        if (response.Result is not List<object> { Count: 100 })
            throw new InvalidDataException("Expected 100 complex objects from callback");
        return $"Complex call took {Stopwatch.ElapsedTicks.FormatElapsedTicks()}";
    }

    public Task<string> PerformIntertwinedCall()
    {
        IntertwinedCompletionSource = new ();
        Stopwatch.Restart();
        IntertwinedCallback!.ExecuteAsync();
        return IntertwinedCompletionSource.Task;
    }

    // ReSharper disable once UnusedMember.Global -- this method is called by Angular
    public void PassDtos(List<SampleDto> sampleDtos)
    {
        Stopwatch.Stop();
        if (sampleDtos.Count is not 100)
            throw new InvalidDataException("Expected 100 complex objects");
        IntertwinedCompletionSource!.SetResult($"Intertwined call took {Stopwatch.ElapsedTicks.FormatElapsedTicks()}");
        IntertwinedCompletionSource = null;
    }
}

// ReSharper disable once ClassNeverInstantiated.Global