using LaConcordia.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public interface IUsersRepository
    {

        //Task AssignRole(EditRoleDTO editRole);
        // Task<List<RoleDTO>> GetRoles();
        Task<List<UserEditDTO>> GetUsuarios();
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO);
        // Task RemoveRole(EditRoleDTO editRole);
    }
}
