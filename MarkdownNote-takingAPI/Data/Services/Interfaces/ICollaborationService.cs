namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface ICollaborationService
    {
        Task<Collaboration> GetById(int CollaborationId);
        Task<List<Collaboration>> GetAllCollaborationsByNoteId(int noteId);
        Task<Collaboration> Create(CreateCollaborationDTO model);
        Task<Collaboration> Update(int CollaborationId, UpdateCollaborationDTO model);
        Task Delete(int CollaborationId);
    }
}
