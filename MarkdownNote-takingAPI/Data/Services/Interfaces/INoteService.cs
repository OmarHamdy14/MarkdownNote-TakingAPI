namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface INoteService
    {
        Task<Note> GetById(int noteId);
        Task<List<Note>> GetAllNotesByUserId(string userId);
        Task<List<Note>> GetAllNotesByUserIdAndCategoryId(string userId, int categoryId);
        Task<Note> Create(CreateNoteDTO model);
        Task<Note> Update(Note note, UpdateNoteDTO model);
        Task Delete(Note note);
        Task<HttpResponseMessage> CheckGrammer(string content);
    }
}
