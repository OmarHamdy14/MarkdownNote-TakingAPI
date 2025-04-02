namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser> FindById(string userId);
        Task<ApplicationUser> FindByEmail(string email);
        Task<ApplicationUser> FindByUserName(string name);
        Task<List<ApplicationUser>> GetAllUsers();
        Task<AuthModel> Register(RegisterUserDTO model);
        Task<AuthModel> GetTokenAsync(LogInDTO model);
        Task<IdentityResult> Update(ApplicationUser user, UpdateUserDTO model);
        Task<bool> ChangePassword(ApplicationUser user, ChangePasswordDTO model);
    }
}
