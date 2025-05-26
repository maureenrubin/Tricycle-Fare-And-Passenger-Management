namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface ISecureStorageService
    {
        #region Public Methods

        Task<T?> GetItemAsync<T>(string key);

        Task SetItemAsync<T>(string key, T value);

        Task RemoveItemAsync(string key);

        Task<bool> IsAvailableAsync();

        #endregion Public Methods
    }
}