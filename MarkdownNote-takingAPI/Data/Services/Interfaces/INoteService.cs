namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface INoteService
    {
        Task<Note> GetById(int noteId, string? IncludeProperties = null);
        Task<NoteFile> GetFileById(int FileId);
        Task<List<Note>> GetAllNotesByUserId(string userId, string? IncludeProperties = null);
        Task<List<Note>> GetAllNotesByUserIdAndCategoryId(string userId, int categoryId, string? IncludeProperties = null);
        Task<Note> Create(CreateNoteDTO model);
        Task<string> UploadFiles(Note note, IFormFile file);
        Task<Note> Update(Note note, UpdateNoteDTO model);
        Task Delete(Note note);
        Task<HttpResponseMessage> CheckGrammer(string content);
        Task DeleteFile(NoteFile file);
    }
}
