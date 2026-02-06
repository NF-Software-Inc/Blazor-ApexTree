using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages;

public partial class Home : ComponentBase
{
	[Inject]
	private NavigationManager Navigator { get; init; } = default!;
}
