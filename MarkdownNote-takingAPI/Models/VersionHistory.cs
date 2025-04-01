namespace MarkdownNote_takingAPI.Models
{
    public class VersionHistory
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
