namespace MarkdownNote_takingAPI.Data.DTOs.NoteDTOs
{
    public class UpdateNoteDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public bool IsPinned { get; set; }
    }
}
