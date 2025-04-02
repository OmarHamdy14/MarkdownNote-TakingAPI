namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Settings> GetByUserId(string userId);
        Task<Settings> Update(Settings Settings, UpdateSettingsDTO model);
    }
}
