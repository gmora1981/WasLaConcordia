namespace LaConcordia.DTO.Auth
{
    public class PermissionSummaryDTO
    {
        public int TotalUsers { get; set; }
        public int UsersWithPermissions { get; set; }
        public int TotalRoles { get; set; }
        public int RolesWithPermissions { get; set; }
        public int TotalNavigationItems { get; set; }
        public int ProtectedItems { get; set; }
        public Dictionary<string, int> PermissionTypeCount { get; set; } = new();
    }

    public class PermissionChangeDTO
    {
        public string EntityType { get; set; } = string.Empty; // "User" or "Role"
        public string EntityId { get; set; } = string.Empty;
        public int NavigationItemId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public string? Reason { get; set; }
    }

    public class PermissionComparisonDTO
    {
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public List<PermissionDifferenceDTO> Differences { get; set; } = new();
    }

    public class PermissionDifferenceDTO
    {
        public int NavigationItemId { get; set; }
        public string NavigationItemTitle { get; set; } = string.Empty;
        public string PermissionType { get; set; } = string.Empty;
        public bool OldValue { get; set; }
        public bool NewValue { get; set; }
    }

    public class PermissionTemplateDTO
    {
        public string TemplateId { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<int, PermissionSetDTO> Permissions { get; set; } = new();
    }

    public class PermissionSetDTO
    {
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
