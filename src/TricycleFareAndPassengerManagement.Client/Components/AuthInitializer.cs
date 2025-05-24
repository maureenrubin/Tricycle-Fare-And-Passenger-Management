using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TricycleFareAndPassengerManagement.Client.Security;

namespace TricycleFareAndPassengerManagement.Client.Components
{
    public partial class AuthInitializer : ComponentBase
    {
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && AuthStateProvider is CustomAuthStateProvider customProvider)
            {
                // Refresh auth state after the component has rendered and JS is available
                await customProvider.RefreshAuthStateAsync();
            }
        }
    }
}