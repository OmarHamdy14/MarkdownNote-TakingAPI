namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface IVersionHistoryService
    {
        Task<VersionHistory> GetById(int VId);
        Task<List<VersionHistory>> GetAllVersionsbyNoteId(int noteId);
        Task SaveVersion(Note note);
    }
}
