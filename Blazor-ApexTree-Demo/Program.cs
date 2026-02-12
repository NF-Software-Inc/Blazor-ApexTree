using ApexTree;
using Blazor_ApexTree_Demo;
using Easy_Logger.Providers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#if DEBUG
builder.Logging.ClearProviders();
builder.Logging.AddConsoleLogger(x =>
{
	x.LogLevels = [LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical];
});
#endif

//ApexTreeLicense.SetLicense("your-commercial-license-key-here");

await builder.Build().RunAsync();
