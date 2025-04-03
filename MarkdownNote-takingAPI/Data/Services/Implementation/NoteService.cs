namespace MarkdownNote_takingAPI.Data.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly HttpClient _httpClient;
        private readonly IEntityBaseRepository<Note> _base;
        private readonly IEntityBaseRepository<NoteFile> _baseNoteFiles;
        private readonly IMapper _mapper;
        private readonly string _storagePath;
        public NoteService(IEntityBaseRepository<Note> b, IMapper mapper, IEntityBaseRepository<NoteFile> baseNoteFiles, IConfiguration confg)
        {
            _base = b;
            _mapper = mapper;
            _baseNoteFiles = baseNoteFiles;
            _storagePath = confg["FileStorage:ServerPath"];
            Directory.CreateDirectory(_storagePath);
        }
        public async Task<Note> GetById(int noteId, string? IncludeProperties = null)
        {
            return await _base.Get(n => n.Id == noteId,IncludeProperties);
        }
        public async Task<NoteFile> GetFileById(int FileId)
        {
            return await _baseNoteFiles.Get(f => f.Id == FileId);
        }
        public async Task<List<Note>> GetAllNotesByUserId(string userId, string? IncludeProperties = null)
        {
            return await _base.GetAll(n => n.UserId == userId, IncludeProperties);
        }
        public async Task<List<Note>> GetAllNotesByUserIdAndCategoryId(string userId, int categoryId, string? IncludeProperties = null)
        {
            return await _base.GetAll(n => n.UserId == userId && n.CategoryId == categoryId, IncludeProperties);
        }
        private async Task<string> HandleMarkdown(string markDown)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(markDown, pipeline);
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
            model.Content = await HandleMarkdown(model.Content);
            var note = _mapper.Map<Note>(model);
            note.CreatedAt = DateTime.UtcNow;
            await _base.Create(note);
            return note;
        }
        public async Task<string> UploadFiles(Note note, IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var noteFile = new NoteFile()
            {
                NoteId = note.Id,
                FileUrl = $"/Uploads/{fileName}",
                FileName = fileName
            };
            await _baseNoteFiles.Create(noteFile);
            return noteFile.FileUrl;
        }
        public async Task<Note> Update(Note note, UpdateNoteDTO model)
        {
            model.Content = await HandleMarkdown(model.Content);

            _mapper.Map(note, model);
            note.UpdatedAt = DateTime.UtcNow;
            await _base.Update(note);
            return note;
        }
        public async Task Delete(Note note)
        {
            await _base.Remove(note);
        }
        public async Task DeleteFile(NoteFile file)
        {
            var filePath = Path.Combine(_storagePath,file.FileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            await _baseNoteFiles.Remove(file);
        }
    }
}
