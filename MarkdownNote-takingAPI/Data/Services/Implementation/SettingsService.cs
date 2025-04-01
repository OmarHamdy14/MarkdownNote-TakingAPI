namespace MarkdownNote_takingAPI.Data.Services.Implementation
{
    public class SettingsService : ISettingsService
    {
        private readonly IEntityBaseRepository<Settings> _base;
        private readonly IMapper _mapper;
        public SettingsService(IEntityBaseRepository<Settings> @base, IMapper mapper)
        {
            _base = @base;
            _mapper = mapper;
        }
        public async Task<Settings> GetByUserId(string userId)
        {
            return await _base.Get(n => n.UserId == userId);
        }
        public async Task<Settings> Update(string userId, UpdateSettingsDTO model)
        {
            var Settings = await _base.Get(n => n.UserId == userId);
            _mapper.Map(Settings, model);
            await _base.Update(Settings);
            return Settings;
        }
    }
}
