# CefSharp vs. WebView2 performance investigation

## Overview

This repo contains three projects: 

- the angular-app that with two components: one that performs I/O with a CefSharp host, and one that does the same with WebView2
- the WPF CefSharpApp that hosts a `ChromiumWebBrowser` control to load the angular app and benchmark I/O with it
- the WPF WebView2App that hosts a `WebView2` control, performing the same benchmarks

To run this example, do the following:

- you need to run on a Windows system, otherwise the WPF clients won't start
- `cd` into the `angular-app` folder and call `npm install` and afterwards `ng serve`
- go into the `CefSharpApp` folder and execute `dotnet run -c Release`
- go into the `WebView2App` folder and execute `dotnet run -c Release`
- click the buttons in both WPF apps to measure the performance simple and complex I/O scenarios

## Conclusion

There is no noticeable performance difference. The first call of each type ("simple", "complex") will take more time than subsequent calls, most likely because with both frameworks, code will be created dynamically (by marshalling, serializers). The following calls range in the lower microseconds ranges (5µs to 90µs) on my dev machine, for both CefSharp and WebView2.