using LaConcordia.Model;
using LaConcordia.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Interface
{
    public interface IRoleManagement
    {
        // CRUD de Roles
        Task<List<RoleDTO>> GetAllRoles();
        Task<PagedResult<RoleDTO>> GetRolesPaginados(int pagina, int pageSize, string? filtro = null);
        Task<RoleDTO> GetRoleById(string roleId);
        Task<RoleDTO> CreateRole(RoleDTO role);
        Task UpdateRole(RoleDTO role);
        Task DeleteRole(string roleId);

        // Gestión de usuarios en roles
        Task<List<UserDTO>> GetUsersInRole(string roleId);
        Task<int> GetUsersCountInRole(string roleId);
        Task AssignUserToRole(EditRoleDTO editRole);
        Task RemoveUserFromRole(EditRoleDTO editRole);
        Task<List<string>> GetUserRoles(string userId);

        // Operaciones masivas
        Task AssignMultipleUsersToRole(string roleId, List<string> userIds);
        Task RemoveMultipleUsersFromRole(string roleId, List<string> userIds);

        // Validaciones
        Task<bool> RoleExists(string roleName);
        Task<bool> CanDeleteRole(string roleId);
    }
}