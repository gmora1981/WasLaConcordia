// IPermissionService.cs - Interfaz del servicio de permisos para el cliente Blazor
using LaConcordia.Model;

namespace LaConcordia.Repository
{
    public interface IPermissionService
    {
        // Permisos de Usuario
        Task<UserPermissionsDto> GetUserPermissionsAsync(string userId);
        Task<NavigationPermissionDto> GetUserPermissionForItemAsync(string userId, int navigationItemId);
        Task UpdateUserPermissionAsync(UpdateUserPermissionDto dto);
        Task RemoveUserPermissionAsync(string userId, int navigationItemId);
        Task AssignBulkUserPermissionsAsync(string userId, BulkPermissionAssignmentDto dto);

        // Permisos de Rol
        Task<RolePermissionsDto> GetRolePermissionsAsync(string roleId);
        Task<NavigationPermissionDto> GetRolePermissionForItemAsync(string roleId, int navigationItemId);
        Task UpdateRolePermissionAsync(UpdateRolePermissionDto dto);
        Task RemoveRolePermissionAsync(string roleId, int navigationItemId);
        Task AssignBulkRolePermissionsAsync(string roleId, BulkPermissionAssignmentDto dto);

        // Permisos Efectivos
        Task<List<NavigationPermissionDto>> GetEffectivePermissionsAsync(string userId);
        Task<NavigationMenuDto> GetUserNavigationMenuAsync(string userId);
        Task<bool> CheckPermissionAsync(CheckPermissionDto dto);

        // Gestión Avanzada
        Task<bool> CopyUserPermissionsAsync(string sourceUserId, string targetUserId);
        Task<bool> CopyRolePermissionsAsync(string sourceRoleId, string targetRoleId);
        Task<bool> ResetUserPermissionsAsync(string userId);
        Task<bool> ResetRolePermissionsAsync(string roleId);

        // Reportes y Auditoría
        Task<List<NavigationPermissionDto>> GetAllNavigationItemsWithPermissionCountAsync();
        Task<List<UserPermissionsDto>> GetAllUsersWithPermissionsAsync();
        Task<PermissionMatrixDto> GetPermissionMatrixAsync();
        Task<PermissionAuditDto> GetPermissionAuditAsync(DateTime startDate, DateTime endDate);
    }

    // DTOs para el sistema de permisos
    public class UserPermissionsDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public List<NavigationPermissionDto> Permissions { get; set; } = new();
    }

    public class NavigationPermissionDto
    {
        public int NavigationItemId { get; set; }
        public string NavigationItemTitle { get; set; } = string.Empty;
        public string? NavigationItemUrl { get; set; }
        public string? NavigationItemIcon { get; set; }
        public int? ParentId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public List<NavigationPermissionDto> Children { get; set; } = new();
    }

    public class RolePermissionsDto
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public List<NavigationPermissionDto> Permissions { get; set; } = new();
    }

    public class NavigationMenuDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool HasAccess { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public List<NavigationMenuDto> Children { get; set; } = new();
    }

    public class UpdateUserPermissionDto
    {
        public string UserId { get; set; } = string.Empty;
        public int NavigationItemId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class UpdateRolePermissionDto
    {
        public string RoleId { get; set; } = string.Empty;
        public int NavigationItemId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class BulkPermissionAssignmentDto
    {
        public List<int> NavigationItemIds { get; set; } = new();
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class CheckPermissionDto
    {
        public string UserId { get; set; } = string.Empty;
        public int NavigationItemId { get; set; }
        public string PermissionType { get; set; } = "View"; // View, Create, Edit, Delete
    }

    public class PermissionMatrixDto
    {
        public List<NavigationItemSummaryDto> NavigationItems { get; set; } = new();
        public List<RoleSummaryDto> Roles { get; set; } = new();
        public List<PermissionMatrixEntryDto> Permissions { get; set; } = new();
    }

    public class NavigationItemSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }

    public class RoleSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class PermissionMatrixEntryDto
    {
        public string RoleId { get; set; } = string.Empty;
        public int NavigationItemId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class PermissionAuditDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PermissionChangeDto> UserPermissionChanges { get; set; } = new();
        public List<PermissionChangeDto> RolePermissionChanges { get; set; } = new();
    }

    public class PermissionChangeDto
    {
        public string EntityId { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public int NavigationItemId { get; set; }
        public string NavigationItemTitle { get; set; } = string.Empty;
        public DateTime GrantedAt { get; set; }
        public string GrantedBy { get; set; } = string.Empty;
    }
}