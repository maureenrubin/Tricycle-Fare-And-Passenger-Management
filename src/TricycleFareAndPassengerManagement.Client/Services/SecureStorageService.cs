using Blazored.LocalStorage;
using Microsoft.JSInterop;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        #region Fields

        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<SecureStorageService> _logger;

        #endregion Fields

        #region Public Constructors

        public SecureStorageService(ILocalStorageService localStorage, IJSRuntime jsRuntime, ILogger<SecureStorageService> logger)
        {
            _localStorage = localStorage;
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> IsAvailableAsync()
        {
            try
            {
                // Check if JavaScript interop is available
                await _jsRuntime.InvokeVoidAsync("eval", "void(0)");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            try
            {
                if (!await IsAvailableAsync())
                    return default(T);

                return await _localStorage.GetItemAsync<T>(key);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get item from storage: {Key}", key);
                return default(T);
            }
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            try
            {
                if (!await IsAvailableAsync())
                    return;

                await _localStorage.SetItemAsync(key, value);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to set item in storage: {Key}", key);
            }
        }

        public async Task RemoveItemAsync(string key)
        {
            try
            {
                if (!await IsAvailableAsync())
                    return;

                await _localStorage.RemoveItemAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to remove item from storage: {Key}", key);
            }
        }

        #endregion Public Methods
    }
}