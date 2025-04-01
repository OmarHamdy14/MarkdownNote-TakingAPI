namespace MarkdownNote_takingAPI.Data.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly HttpClient _httpClient;
        private readonly IEntityBaseRepository<Note> _base;
        private readonly IMapper _mapper;
        public NoteService(IEntityBaseRepository<Note> b, IMapper mapper)
        {
            _base = b;
            _mapper = mapper;
        }
        public async Task<Note> GetById(int noteId)
        {
            return await _base.Get(n => n.Id == noteId);
        }
        public async Task<List<Note>> GetAllNotesByUserId(string userId)
        {
            return await _base.GetAll(n => n.UserId == userId);
        }
        public async Task<List<Note>> GetAllNotesByUserIdAndCategoryId(string userId,int categoryId)
        {
            return await _base.GetAll(n => n.UserId == userId && n.CategoryId == categoryId);
        }
        public async Task<string> ConvertMarkdown(string markDown)
        {
            return Markdown.ToHtml(markDown);
        }
        public async Task<HttpResponseMessage> CheckGrammer(string content)
        {
            var apiURL = "https://api.languagetool.org/v2/check";
            var data = new
            {
                text = content,
                language = "en-US"
            };
            var PostData = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, PostData);

            return response;
        }
        public async Task<Note> Create(CreateNoteDTO model)
        {
            model.Content = await ConvertMarkdown(model.Content);
            var note = _mapper.Map<Note>(model);
            note.CreatedAt = DateTime.UtcNow;
            await _base.Create(note);
            return note;
        } 
        public async Task<Note> Update(int noteId, UpdateNoteDTO model)
        {
            model.Content = await ConvertMarkdown(model.Content);

            var note = await _base.Get(n => n.Id == noteId);
            _mapper.Map(note, model);
            note.UpdatedAt = DateTime.UtcNow;
            await _base.Update(note);
            return note;
        }
        public async Task Delete(int noteId)
        {
            var note = await _base.Get(n => n.Id == noteId);
            await _base.Remove(note);
        }
    }
}
