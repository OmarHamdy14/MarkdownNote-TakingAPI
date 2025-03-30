namespace MarkdownNote_takingAPI.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public bool Theme { get; set; }
        public int FontSize { get; set; }
        public int AutoSave_Interval { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}