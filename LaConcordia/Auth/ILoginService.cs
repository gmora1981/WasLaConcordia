using LaConcordia.Model;

namespace LaConcordia.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task TryRenewToken();

        // Nuevos métodos para manejar roles
        Task<string> GetUserRoleAsync();
        Task<string[]> GetUserRolesAsync();
        Task<UserInfo> GetUserInfoAsync();
    }
}
