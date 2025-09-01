using LaConcordia.Interface;
using LaConcordia.Model;
using LaConcordia.DTO;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public class RoleManagementRepository : IRoleManagement
    {
        private readonly HttpClient _httpClient;

        public RoleManagementRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoleDTO>> GetAllRoles()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RoleDTO>>("api/users/roles")
                    ?? new List<RoleDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener todos los roles", ex);
            }
        }

        public async Task<PagedResult<RoleDTO>> GetRolesPaginados(int pagina, int pageSize, string? filtro = null)
        {
            var url = $"api/roles/GetRolesPaginados?pagina={pagina}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(filtro))
                url += $"&filtro={Uri.EscapeDataString(filtro)}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<RoleDTO>>(url)
                    ?? new PagedResult<RoleDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener roles paginados", ex);
            }
        }

        public async Task<RoleDTO> GetRoleById(string roleId)
        {
            try
            {
                var role = await _httpClient.GetFromJsonAsync<RoleDTO>($"api/roles/{roleId}");
                return role ?? throw new Exception("Rol no encontrado");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener rol con ID {roleId}", ex);
            }
        }

        public async Task<RoleDTO> CreateRole(RoleDTO role)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/roles/CreateRole", role);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (errorContent.Contains("ya existe"))
                        throw new Exception("Ya existe un rol con ese nombre");

                    throw new Exception($"Error al crear rol: {errorContent}");
                }

                var createdRole = await response.Content.ReadFromJsonAsync<RoleDTO>();
                return createdRole ?? throw new Exception("Error al obtener el rol creado");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al crear rol", ex);
            }
        }

        public async Task UpdateRole(RoleDTO role)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/roles/{role.RoleId}", role);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar rol: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al actualizar rol", ex);
            }
        }

        public async Task DeleteRole(string roleId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/roles/{roleId}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (errorContent.Contains("usuarios asignados"))
                        throw new Exception("No se puede eliminar el rol porque tiene usuarios asignados");

                    if (errorContent.Contains("rol del sistema"))
                        throw new Exception("No se puede eliminar un rol del sistema");

                    throw new Exception($"Error al eliminar rol: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al eliminar rol", ex);
            }
        }

        public async Task<List<UserDTO>> GetUsersInRole(string roleId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<RoleUsersResponse>(
                    $"api/permissions/role/{roleId}/users");

                return response?.Users ?? new List<UserDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener usuarios del rol {roleId}", ex);
            }
        }

        public async Task<int> GetUsersCountInRole(string roleId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/roles/{roleId}/users/count");

                if (!response.IsSuccessStatusCode)
                    return 0;

                return await response.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return 0;
            }
        }

        public async Task AssignUserToRole(EditRoleDTO editRole)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/users/assignRole", editRole);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (errorContent.Contains("ya tiene"))
                        throw new Exception("El usuario ya tiene este rol asignado");

                    throw new Exception($"Error al asignar rol: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al asignar rol", ex);
            }
        }

        public async Task RemoveUserFromRole(EditRoleDTO editRole)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/users/removeRole", editRole);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (errorContent.Contains("último administrador"))
                        throw new Exception("No se puede quitar el rol Admin del último administrador");

                    throw new Exception($"Error al remover rol: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al remover rol", ex);
            }
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<string>>($"api/users/{userId}/roles")
                    ?? new List<string>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener roles del usuario {userId}", ex);
            }
        }

        public async Task AssignMultipleUsersToRole(string roleId, List<string> userIds)
        {
            try
            {
                var request = new { RoleId = roleId, UserIds = userIds };
                var response = await _httpClient.PostAsJsonAsync($"api/roles/{roleId}/assign-multiple", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al asignar múltiples usuarios al rol: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al asignar múltiples usuarios", ex);
            }
        }

        public async Task RemoveMultipleUsersFromRole(string roleId, List<string> userIds)
        {
            try
            {
                var request = new { RoleId = roleId, UserIds = userIds };
                var response = await _httpClient.PostAsJsonAsync($"api/roles/{roleId}/remove-multiple", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al remover múltiples usuarios del rol: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al remover múltiples usuarios", ex);
            }
        }

        public async Task<bool> RoleExists(string roleName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/roles/exists/{Uri.EscapeDataString(roleName)}");

                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CanDeleteRole(string roleId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/roles/{roleId}/can-delete");

                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }

        // Clase auxiliar para la respuesta de usuarios en rol
        private class RoleUsersResponse
        {
            public string RoleId { get; set; } = "";
            public int UserCount { get; set; }
            public List<UserDTO> Users { get; set; } = new();
        }
    }
}