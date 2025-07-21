using LaConcordia.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public interface IUsersRepository
    {

        // Métodos básicos de usuario
        Task<List<UserEditDTO>> GetUsuarios();
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO);
        Task<UserDTO> GetUserById(string userId);
        Task UpdateUser(UserEditDTO user);
        Task DeleteUser(string userId);
        Task<bool> CheckEmailExists(string email);
        Task<List<UserDTO>> SearchUsers(string searchTerm);

        // Gestión de roles
        Task<List<RoleDTO>> GetRoles();
        Task<List<string>> GetUserRoles(string userId);
        Task AssignRole(EditRoleDTO editRole);
        Task RemoveRole(EditRoleDTO editRole);
        Task<List<RoleInfoDTO>> GetRolesInfo();

        // Gestión de contraseñas
        Task ChangeUserPassword(string userId, ChangePasswordDTO changePassword);
    }
}
