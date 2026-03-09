using ReiDoChopp.DataTransfer.Settings;

namespace ReiDoChopp.Application.Settings.Services
{
    public interface ISettingsAppService
    {
        Task ResetStockingHistory(SettingsAuthRequest request);
        Task ResetBase(SettingsAuthRequest request);
    }
}
