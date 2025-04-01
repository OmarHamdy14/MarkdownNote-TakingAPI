
namespace MarkdownNote_takingAPI.Models
{
    public class Collaboration
    {
        public int Id { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }
        public Note Note { get; set; }
        [ForeignKey("Collaborator")]
        public string CollaboratorId { get; set; }
        public ApplicationUser Collaborator { get; set; }
        public bool Permission { get; set; } // 1 => Read&Write , 0 => ReadOnly
    }
}
