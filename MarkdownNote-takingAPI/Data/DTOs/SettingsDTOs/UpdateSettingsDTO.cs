namespace MarkdownNote_takingAPI.Data.DTOs.SettingsDTOs
{
    public class UpdateSettingsDTO
    {
        public bool Theme { get; set; }
        public int FontSize { get; set; }
        public int AutoSave_Interval { get; set; }
    }
}
