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

        /*  public async Task<List<RoleDTO>> GetRoles()
          {
              return await httpService.GetHelper<List<RoleDTO>>($"{url}/roles");
          }

          public async Task AssignRole(EditRoleDTO editRole)
          {
              var response = await httpService.Post($"{url}/assignRole", editRole);
              if (!response.Success)
              {
                  throw new ApplicationException(await response.GetBody());
              }
          }

          public async Task RemoveRole(EditRoleDTO editRole)
          {
              var response = await httpService.Post($"{url}/removeRole", editRole);
              if (!response.Success)
              {
                  throw new ApplicationException(await response.GetBody());
              }
          }

          */
    }
}
