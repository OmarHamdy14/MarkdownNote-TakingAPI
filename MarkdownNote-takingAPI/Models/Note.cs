namespace MarkdownNote_takingAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Note can only be in one category (to make it be in more, make a table "Note-Tags" ==> Many-to-Many)
        public bool IsPinned { get; set; }
        public ICollection<NoteFile> Files { get; set; }
    }
}
