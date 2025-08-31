using LaConcordia.Model;

namespace LaConcordia.DTO
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string EntityId { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty; // User or Role
        public int NavigationItemId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class UserPermissionDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<PermissionItemDTO> Permissions { get; set; } = new();
    }

    public class RolePermissionDTO
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int UserCount { get; set; }
        public List<PermissionItemDTO> Permissions { get; set; } = new();
    }

    public class PermissionItemDTO
    {
        public int NavigationItemId { get; set; }
        public string NavigationItemTitle { get; set; } = string.Empty;
        public string? NavigationItemUrl { get; set; }
        public string? NavigationItemIcon { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class BulkPermissionDTO
    {
        public List<string> EntityIds { get; set; } = new();
        public string EntityType { get; set; } = "User";
        public List<int> NavigationItemIds { get; set; } = new();
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool ReplaceExisting { get; set; }
    }

    public class CopyPermissionDTO
    {
        public string SourceEntityId { get; set; } = string.Empty;
        public string TargetEntityId { get; set; } = string.Empty;
        public string EntityType { get; set; } = "User";
        public bool ReplaceExisting { get; set; }
        public bool MergePermissions { get; set; }
    }

    public class PermissionMatrixDTO
    {
        public List<NavigationItemDTO> NavigationItems { get; set; } = new();
        public List<RoleDTO> Roles { get; set; } = new();
        public Dictionary<string, List<PermissionItemDTO>> RolePermissions { get; set; } = new();
    }

    public class PermissionAuditEntryDTO
    {
        public DateTime Date { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = string.Empty;
        public string NavigationItem { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public List<string> Changes { get; set; } = new();
    }

    public class PermissionReportDTO
    {
        public string ReportType { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }
        public int TotalUsers { get; set; }
        public int UsersWithPermissions { get; set; }
        public int TotalRoles { get; set; }
        public int RolesWithPermissions { get; set; }
        public int TotalNavigationItems { get; set; }
        public int ProtectedItems { get; set; }
        public List<PermissionStatDTO> Statistics { get; set; } = new();
    }

    public class PermissionStatDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }
}