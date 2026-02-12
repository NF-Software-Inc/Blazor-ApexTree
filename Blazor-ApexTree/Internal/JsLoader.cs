using Microsoft.JSInterop;

namespace ApexTree.Internal;

internal static class JsLoader
{
	internal static async Task<IJSObjectReference> LoadAsync(IJSRuntime jsRuntime, string? path = null)
	{
		var javascriptPath = "./_content/Blazor-ApexTree/js/blazor-apextree.js?ver=10.1.0";

		if (string.IsNullOrWhiteSpace(path) == false)
			javascriptPath = path;

		var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", javascriptPath);
		return await module.InvokeAsync<IJSObjectReference>("GetApexTree");
	}
}
