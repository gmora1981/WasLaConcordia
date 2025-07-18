using LaConcordia.Helpers;
using LaConcordia.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public class UserRepository : IUsersRepository
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api";

        public UserRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO)
        {
            return await httpService.GetHelper<List<UserDTO>>($"{baseURL}/users", paginationDTO);
        }

        public async Task<List<UserEditDTO>> GetUsuarios()
        {
            return await httpService.GetHelper<List<UserEditDTO>>($"{baseURL}/users/usuarios");
        }
        // Nuevos métodos para gestión de roles
        public async Task<List<RoleDTO>> GetRoles()
        {
            return await httpService.GetHelper<List<RoleDTO>>($"{baseURL}/users/roles");
        }

        public async Task AssignRole(EditRoleDTO editRole)
        {
            var response = await httpService.Post<EditRoleDTO, object>($"{baseURL}/users/assignRole", editRole);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task RemoveRole(EditRoleDTO editRole)
        {
            var response = await httpService.Post<EditRoleDTO, object>($"{baseURL}/users/removeRole", editRole);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        // Métodos adicionales para gestión completa de usuarios
        public async Task<UserDTO> GetUserById(string userId)
        {
            return await httpService.GetHelper<UserDTO>($"{baseURL}/users/{userId}");
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            return await httpService.GetHelper<List<string>>($"{baseURL}/users/{userId}/roles");
        }

        public async Task UpdateUser(UserEditDTO user)
        {
            var response = await httpService.Put<UserEditDTO, object>($"{baseURL}/users/{user.UserId}", user);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteUser(string userId)
        {
            var response = await httpService.Delete($"{baseURL}/users/{userId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await httpService.GetHelper<bool>($"{baseURL}/users/check-email/{email}");
        }

        public async Task ChangeUserPassword(string userId, ChangePasswordDTO changePassword)
        {
            var response = await httpService.Post<ChangePasswordDTO, object>($"{baseURL}/users/{userId}/change-password", changePassword);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<UserDTO>> SearchUsers(string searchTerm)
        {
            return await httpService.GetHelper<List<UserDTO>>($"{baseURL}/users/search?term={searchTerm}");
        }

    }
}
