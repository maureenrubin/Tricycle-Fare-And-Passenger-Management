using Microsoft.JSInterop;
using Blazored.LocalStorage;

public class AuthTokenHandler : DelegatingHandler
{
    #region Fields

    private readonly ILocalStorageService _localStorage;
    private readonly IJSRuntime _jsRuntime;

    #endregion Fields

    #region Public Constructors

    public AuthTokenHandler(ILocalStorageService localStorage, IJSRuntime jsRuntime)
    {
        _localStorage = localStorage;
        _jsRuntime = jsRuntime;
    }

    #endregion Public Constructors

    #region Protected Methods

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            if (_jsRuntime is IJSInProcessRuntime ||
                (_jsRuntime is not null && !((IJSRuntime) _jsRuntime).GetType().Name.Contains("UnsupportedJavaScriptRuntime")))
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
        }
        catch (InvalidOperationException)
        {
            // JavaScript interop not available during prerendering - skip token retrieval
        }

        return await base.SendAsync(request, cancellationToken);
    }

    #endregion Protected Methods
}