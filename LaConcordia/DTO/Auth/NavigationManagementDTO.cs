namespace LaConcordia.DTO.Auth
{
    public class NavigationItemManagementDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public string? RequiredRole { get; set; }
        public List<NavigationItemManagementDTO> Children { get; set; } = new();
        public int PermissionCount { get; set; }
        public bool HasChildren { get; set; }
    }

    public class NavigationFilterDTO
    {
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; }
        public int? ParentId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class NavigationReorderDTO
    {
        public int ItemId { get; set; }
        public int NewOrder { get; set; }
        public int? NewParentId { get; set; }
    }

    public class NavigationBulkOperationDTO
    {
        public List<int> ItemIds { get; set; } = new();
        public string Operation { get; set; } = string.Empty; // "activate", "deactivate", "delete", "setRole"
        public string? RequiredRole { get; set; }
    }
}
