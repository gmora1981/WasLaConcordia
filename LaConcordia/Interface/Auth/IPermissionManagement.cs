using LaConcordia.Model;
using LaConcordia.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Interface
{
    public interface IPermissionManagement
    {
        // Permisos de Usuario
        Task<UserPermissionsDto> GetUserPermissions(string userId);
        Task<NavigationPermissionDto> GetUserPermissionForItem(string userId, int navigationItemId);
        Task UpdateUserPermission(UpdateUserPermissionDto dto);
        Task RemoveUserPermission(string userId, int navigationItemId);
        Task AssignBulkUserPermissions(string userId, BulkPermissionAssignmentDto dto);

        // Permisos de Rol
        Task<RolePermissionsDto> GetRolePermissions(string roleId);
        Task<NavigationPermissionDto> GetRolePermissionForItem(string roleId, int navigationItemId);
        Task UpdateRolePermission(UpdateRolePermissionDto dto);
        Task RemoveRolePermission(string roleId, int navigationItemId);
        Task AssignBulkRolePermissions(string roleId, BulkPermissionAssignmentDto dto);

        // Permisos Efectivos
        Task<List<NavigationPermissionDto>> GetEffectivePermissions(string userId);
        Task<bool> CheckPermission(CheckPermissionDto dto);
        Task<bool> UserHasPermission(string userId, int navigationItemId, string permissionType);

        // Copia y Reseteo de Permisos
        Task<bool> CopyUserPermissions(string sourceUserId, string targetUserId);
        Task<bool> CopyRolePermissions(string sourceRoleId, string targetRoleId);
        Task<bool> CopyRoleToUser(string roleId, string userId);
        Task<bool> ResetUserPermissions(string userId);
        Task<bool> ResetRolePermissions(string roleId);

        // Matriz de Permisos
        Task<PermissionMatrixDto> GetPermissionMatrix();
        Task UpdatePermissionMatrix(List<PermissionMatrixEntryDto> entries);

        // Reportes
        Task<List<UserPermissionsDto>> GetAllUsersWithPermissions();
        Task<List<NavigationPermissionDto>> GetAllNavigationItemsWithPermissionCount();
        Task<PermissionAuditDto> GetPermissionAudit(DateTime startDate, DateTime endDate);

        // Exportación
        Task<byte[]> ExportPermissionsToExcel();
        Task<byte[]> ExportPermissionsToPdf();
        Task<byte[]> ExportPermissionMatrixToCsv();
    }
}