namespace LaConcordia.DTO.Auth
{
    public class UserManagementDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public class UserFilterDTO
    {
        public string? SearchTerm { get; set; }
        public string? RoleFilter { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class UserRoleAssignmentDTO
    {
        public string UserId { get; set; } = string.Empty;
        public List<string> RolesToAdd { get; set; } = new();
        public List<string> RolesToRemove { get; set; } = new();
    }

    public class BulkUserOperationDTO
    {
        public List<string> UserIds { get; set; } = new();
        public string Operation { get; set; } = string.Empty; // "activate", "deactivate", "delete", "assignRole"
        public string? RoleName { get; set; }
    }
}