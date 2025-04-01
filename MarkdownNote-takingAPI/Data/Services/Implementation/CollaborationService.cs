namespace MarkdownNote_takingAPI.Data.Services.Implementation
{
    public class CollaborationService : ICollaborationService
    {
        private readonly IEntityBaseRepository<Collaboration> _base;
        private readonly IMapper _mapper;   
        public CollaborationService(IEntityBaseRepository<Collaboration> @base, IMapper mapper)
        {
            _base = @base;
            _mapper = mapper;
        }
        public async Task<Collaboration> GetById(int CollaborationId)
        {
            return await _base.Get(n => n.Id == CollaborationId);
        }
        public async Task<List<Collaboration>> GetAllCollaborationsByNoteId(int noteId)
        {
            return await _base.GetAll(n => n.NoteId == noteId);
        }
        public async Task<Collaboration> Create(CreateCollaborationDTO model)
        {
            var Collaboration = _mapper.Map<Collaboration>(model);
            await _base.Create(Collaboration);
            return Collaboration;
        }
        public async Task<Collaboration> Update(int CollaborationId, UpdateCollaborationDTO model)
        {
            var Collaboration = await _base.Get(n => n.Id == CollaborationId);
            _mapper.Map(Collaboration, model);
            await _base.Update(Collaboration);
            return Collaboration;
        }
        public async Task Delete(int CollaborationId)
        {
            var Collaboration = await _base.Get(n => n.Id == CollaborationId);
            await _base.Remove(Collaboration);
        }
    }
}
