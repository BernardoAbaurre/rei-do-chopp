using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Users.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> ValidateAsync(int id);
        Task<User> InsertAsync(string firstName, string lastName, string email, string password);
        Task<string> LoginAsync(string email, string password);
        void MatchPassword(string password, string confirmPassword);
        Task ForgotPasswordAsync(User user);
        Task ResetPasswordAsync(User user, string resetToken, string password);
        Task<User> GetCurrentUserAsync();
        Task<User> SetUserRolesAsync(User user, int[] rolesIds);
        Task<User> UserEditAsync(User user, string email, string firstName, string LastName);
        Task<IList<string>> GetRolesByUserAsync(User user);
        User ChangeStatus(User user);
        Task<User> GetUserByEmail(string email);
        Task<bool> ConfirmCurrentUserPassword(string password);
    }
}
