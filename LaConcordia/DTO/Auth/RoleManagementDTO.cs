namespace LaConcordia.DTO.Auth
{
    public class RoleManagementDTO
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int UserCount { get; set; }
        public bool IsSystemRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Permissions { get; set; } = new();
    }

    public class CreateRoleDTO
    {
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<int> NavigationItemIds { get; set; } = new();
    }

    public class UpdateRoleDTO
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class RoleFilterDTO
    {
        public string? SearchTerm { get; set; }
        public bool? IsSystemRole { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
