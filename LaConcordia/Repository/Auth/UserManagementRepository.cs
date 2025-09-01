using LaConcordia.Interface;
using LaConcordia.Model;
using LaConcordia.DTO;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public class UserManagementRepository : IUserManagement
    {
        private readonly HttpClient _httpClient;

        public UserManagementRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResult<UserDTO>> GetUsersPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            var url = $"api/users/GetUsersPaginados?pagina={pagina}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(filtro))
                url += $"&filtro={Uri.EscapeDataString(filtro)}";

            if (!string.IsNullOrEmpty(estado))
                url += $"&estado={Uri.EscapeDataString(estado)}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<UserDTO>>(url)
                    ?? new PagedResult<UserDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener usuarios paginados", ex);
            }
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/users/GetAllUsers")
                    ?? new List<UserDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener todos los usuarios", ex);
            }
        }

        public async Task<UserDTO> GetUserById(string userId)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<UserDTO>($"api/users/{userId}");
                return user ?? throw new Exception("Usuario no encontrado");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener usuario con ID {userId}", ex);
            }
        }

        public async Task<UserDTO> CreateUser(UserEditDTO userInfo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/users/CreateUser", userInfo);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear usuario: {errorContent}");
                }

                var createdUser = await response.Content.ReadFromJsonAsync<UserDTO>();
                return createdUser ?? throw new Exception("Error al obtener el usuario creado");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al crear usuario", ex);
            }
        }

        public async Task UpdateUser(UserEditDTO userInfo)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/users/{userInfo.UserId}", userInfo);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        throw new Exception("Usuario no encontrado");

                    throw new Exception($"Error al actualizar usuario: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al actualizar usuario", ex);
            }
        }

        public async Task DeleteUser(string userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/users/{userId}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (errorContent.Contains("propio usuario"))
                        throw new Exception("No puede eliminar su propio usuario");

                    if (errorContent.Contains("último administrador"))
                        throw new Exception("No se puede eliminar el último administrador del sistema");

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        throw new Exception("Usuario no encontrado");

                    throw new Exception($"Error al eliminar usuario: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al eliminar usuario", ex);
            }
        }

        public async Task<List<UserDTO>> SearchUsers(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return new List<UserDTO>();

                var url = $"api/users/search?term={Uri.EscapeDataString(searchTerm)}";
                return await _httpClient.GetFromJsonAsync<List<UserDTO>>(url)
                    ?? new List<UserDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al buscar usuarios con término '{searchTerm}'", ex);
            }
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/users/check-email/{Uri.EscapeDataString(email)}");

                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al verificar email {email}", ex);
            }
        }

        public async Task<bool> ValidateUser(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/users/validate/{userId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<byte[]> ExportUsersToExcel(string? filtro = null)
        {
            try
            {
                var url = $"api/users/export/excel";
                if (!string.IsNullOrEmpty(filtro))
                    url += $"?filtro={Uri.EscapeDataString(filtro)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al exportar a Excel: {errorContent}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al exportar usuarios a Excel", ex);
            }
        }

        public async Task<byte[]> ExportUsersToPdf(string? filtro = null)
        {
            try
            {
                var url = $"api/users/export/pdf";
                if (!string.IsNullOrEmpty(filtro))
                    url += $"?filtro={Uri.EscapeDataString(filtro)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al exportar a PDF: {errorContent}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al exportar usuarios a PDF", ex);
            }
        }
    }
}