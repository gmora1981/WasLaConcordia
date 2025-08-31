using LaConcordia.Model;
using LaConcordia.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Interface
{
    public interface IUserManagement
    {
        // CRUD de Usuarios
        Task<PagedResult<UserDTO>> GetUsersPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(string userId);
        Task<UserDTO> CreateUser(UserEditDTO userInfo);
        Task UpdateUser(UserEditDTO userInfo);
        Task DeleteUser(string userId);

        // Búsqueda y validación
        Task<List<UserDTO>> SearchUsers(string searchTerm);
        Task<bool> CheckEmailExists(string email);
        Task<bool> ValidateUser(string userId);

        // Exportación
        Task<byte[]> ExportUsersToExcel(string? filtro = null);
        Task<byte[]> ExportUsersToPdf(string? filtro = null);
    }
}