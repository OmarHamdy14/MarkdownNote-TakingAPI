namespace MarkdownNote_takingAPI.Data.Services.Implementation
{
    public class VersionHistoryService : IVersionHistoryService
    {
        private readonly IEntityBaseRepository<VersionHistory> _base;
        private readonly IEntityBaseRepository<Note> _baseNote;
        public VersionHistoryService(IEntityBaseRepository<VersionHistory> @base, IEntityBaseRepository<Note> baseNote)
        {
            _base = @base;
            _baseNote = baseNote;   
        }
        public async Task<VersionHistory> GetById(int VId)
        {
            return await _base.Get(v => v.Id == VId);
        }
        public async Task<List<VersionHistory>> GetAllVersionsbyNoteId(int noteId)
        {
            return await _base.GetAll(v => v.NoteId == noteId);
        }
        public async Task SaveVersion(Note note)
        {
            var version = new VersionHistory
            {
                Content = note.Content,
                Timestamp = DateTime.UtcNow,
                NoteId = note.Id
            };
            await _base.Create(version);
        }
    }
}
