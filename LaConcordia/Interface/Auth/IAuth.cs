using LaConcordia.Model;
using System.Threading.Tasks;

namespace LaConcordia.Interface
{
    public interface IAuth
    {
        // Login y Registro
        Task<UserToken> Login(UserLogin userLogin);
        Task<UserToken> Register(UserEditDTO userInfo);
        Task<UserToken> RenewToken();
        Task Logout();

        // Gestión de contraseñas
        Task<bool> ChangePassword(string userId, ChangePasswordDTO changePassword);
        Task<bool> ResetPassword(string email);

        // Verificación de sesión
        Task<bool> IsAuthenticated();
        Task<UserInfo> GetCurrentUser();
    }
}