namespace MarkdownNote_takingAPI.Data.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Note, CreateNoteDTO>().ReverseMap();
            CreateMap<Note, UpdateNoteDTO>().ReverseMap();

            CreateMap<Collaboration, CreateCollaborationDTO>().ReverseMap();
            CreateMap<Collaboration, UpdateCollaborationDTO>().ReverseMap();

            CreateMap<Settings, UpdateSettingsDTO>().ReverseMap();

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDTO>().ReverseMap();
        }
    }
}
