using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CefSharp;

namespace CefSharpApp;

public sealed class BoundObject
{
    private IJavascriptCallback? SimpleCallback { get; set; }
    private IJavascriptCallback? ComplexCallback { get; set; }

    // ReSharper disable once UnusedMember.Global -- called by Angular
    public void SetUp(IJavascriptCallback simpleCallback, IJavascriptCallback complexCallback)
    {
        SimpleCallback = simpleCallback;
        ComplexCallback = complexCallback;
    }

    public async Task PerformSimpleCallAsync()
    {
        var response = await SimpleCallback!.ExecuteAsync();
        if ((int) response.Result != 42)
            throw new InvalidDataException("Expected 42 from simple callback");
    }

    public async Task PerformComplexCall()
    {
        var response = await ComplexCallback!.ExecuteAsync();
        if (response.Result is not List<object> { Count: 100 })
            throw new InvalidDataException("Expected 100 complex objects from callback");
    }
}