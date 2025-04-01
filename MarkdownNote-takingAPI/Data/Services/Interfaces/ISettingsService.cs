namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Settings> GetByUserId(string userId);
        Task<Settings> Update(string userId, UpdateSettingsDTO model);
    }
}
