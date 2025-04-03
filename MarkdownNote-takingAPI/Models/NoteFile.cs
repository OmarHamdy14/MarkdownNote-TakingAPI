namespace MarkdownNote_takingAPI.Models
{
    public class NoteFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
