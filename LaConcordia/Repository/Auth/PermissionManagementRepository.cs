using LaConcordia.Interface;
using LaConcordia.Model;
using LaConcordia.Repository;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public class PermissionManagementRepository : IPermissionManagement
    {
        private readonly HttpClient _httpClient;
        private readonly IPermissionService _permissionService;

        public PermissionManagementRepository(HttpClient httpClient, IPermissionService permissionService)
        {
            _httpClient = httpClient;
            _permissionService = permissionService;
        }

        // Permisos de Usuario
        public async Task<UserPermissionsDto> GetUserPermissions(string userId)
        {
            return await _permissionService.GetUserPermissionsAsync(userId);
        }

        public async Task<NavigationPermissionDto> GetUserPermissionForItem(string userId, int navigationItemId)
        {
            return await _permissionService.GetUserPermissionForItemAsync(userId, navigationItemId);
        }

        public async Task UpdateUserPermission(UpdateUserPermissionDto dto)
        {
            await _permissionService.UpdateUserPermissionAsync(dto);
        }

        public async Task RemoveUserPermission(string userId, int navigationItemId)
        {
            await _permissionService.RemoveUserPermissionAsync(userId, navigationItemId);
        }

        public async Task AssignBulkUserPermissions(string userId, BulkPermissionAssignmentDto dto)
        {
            await _permissionService.AssignBulkUserPermissionsAsync(userId, dto);
        }

        // Permisos de Rol
        public async Task<RolePermissionsDto> GetRolePermissions(string roleId)
        {
            return await _permissionService.GetRolePermissionsAsync(roleId);
        }

        public async Task<NavigationPermissionDto> GetRolePermissionForItem(string roleId, int navigationItemId)
        {
            return await _permissionService.GetRolePermissionForItemAsync(roleId, navigationItemId);
        }

        public async Task UpdateRolePermission(UpdateRolePermissionDto dto)
        {
            await _permissionService.UpdateRolePermissionAsync(dto);
        }

        public async Task RemoveRolePermission(string roleId, int navigationItemId)
        {
            await _permissionService.RemoveRolePermissionAsync(roleId, navigationItemId);
        }

        public async Task AssignBulkRolePermissions(string roleId, BulkPermissionAssignmentDto dto)
        {
            await _permissionService.AssignBulkRolePermissionsAsync(roleId, dto);
        }

        // Permisos Efectivos
        public async Task<List<NavigationPermissionDto>> GetEffectivePermissions(string userId)
        {
            return await _permissionService.GetEffectivePermissionsAsync(userId);
        }

        public async Task<bool> CheckPermission(CheckPermissionDto dto)
        {
            return await _permissionService.CheckPermissionAsync(dto);
        }

        public async Task<bool> UserHasPermission(string userId, int navigationItemId, string permissionType)
        {
            var dto = new CheckPermissionDto
            {
                UserId = userId,
                NavigationItemId = navigationItemId,
                PermissionType = permissionType
            };

            return await CheckPermission(dto);
        }

        // Copia y Reseteo de Permisos
        public async Task<bool> CopyUserPermissions(string sourceUserId, string targetUserId)
        {
            try
            {
                var copyDto = new
                {
                    EntityType = "User",
                    SourceId = sourceUserId,
                    TargetId = targetUserId
                };

                var response = await _httpClient.PostAsJsonAsync("api/permissions/copy/user", copyDto);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CopyRolePermissions(string sourceRoleId, string targetRoleId)
        {
            try
            {
                var copyDto = new
                {
                    EntityType = "Role",
                    SourceId = sourceRoleId,
                    TargetId = targetRoleId
                };

                var response = await _httpClient.PostAsJsonAsync("api/permissions/copy/role", copyDto);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CopyRoleToUser(string roleId, string userId)
        {
            try
            {
                var copyDto = new
                {
                    EntityType = "RoleToUser",
                    SourceId = roleId,
                    TargetId = userId
                };

                var response = await _httpClient.PostAsJsonAsync("api/permissions/copy/role-to-user", copyDto);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ResetUserPermissions(string userId)
        {
            return await _permissionService.ResetUserPermissionsAsync(userId);
        }

        public async Task<bool> ResetRolePermissions(string roleId)
        {
            return await _permissionService.ResetRolePermissionsAsync(roleId);
        }

        // Matriz de Permisos
        public async Task<PermissionMatrixDto> GetPermissionMatrix()
        {
            return await _permissionService.GetPermissionMatrixAsync();
        }

        public async Task UpdatePermissionMatrix(List<PermissionMatrixEntryDto> entries)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/permissions/matrix/update", entries);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar matriz de permisos: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al actualizar matriz de permisos", ex);
            }
        }

        // Reportes
        public async Task<List<UserPermissionsDto>> GetAllUsersWithPermissions()
        {
            return await _permissionService.GetAllUsersWithPermissionsAsync();
        }

        public async Task<List<NavigationPermissionDto>> GetAllNavigationItemsWithPermissionCount()
        {
            return await _permissionService.GetAllNavigationItemsWithPermissionCountAsync();
        }

        public async Task<PermissionAuditDto> GetPermissionAudit(DateTime startDate, DateTime endDate)
        {
            return await _permissionService.GetPermissionAuditAsync(startDate, endDate);
        }

        // Exportación
        public async Task<byte[]> ExportPermissionsToExcel()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/permissions/export/excel");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al exportar permisos a Excel: {errorContent}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al exportar permisos a Excel", ex);
            }
        }

        public async Task<byte[]> ExportPermissionsToPdf()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/permissions/export/pdf");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al exportar permisos a PDF: {errorContent}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al exportar permisos a PDF", ex);
            }
        }

        public async Task<byte[]> ExportPermissionMatrixToCsv()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/permissions/matrix/export/csv");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al exportar matriz de permisos a CSV: {errorContent}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al exportar matriz de permisos a CSV", ex);
            }
        }
    }
}