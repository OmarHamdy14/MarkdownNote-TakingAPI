namespace MarkdownNote_takingAPI.Data.DTOs.CollaborationDTOs
{
    public class CreateCollaborationDTO
    {
        public int NoteId { get; set; }
        public string CollaboratorId { get; set; }
        public bool Permission { get; set; } // 1 => Read&Write , 0 => ReadOnly
    }
}
