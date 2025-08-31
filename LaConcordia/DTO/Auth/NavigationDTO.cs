namespace LaConcordia.DTO
{
    public class NavigationItemDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public string? RequiredRole { get; set; }
        public List<NavigationItemDTO> Children { get; set; } = new();
    }

    public class CreateNavigationItemDTO
    {
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RequiredRole { get; set; }
    }

    public class UpdateNavigationItemDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public string? RequiredRole { get; set; }
    }

    public class NavigationTreeDTO
    {
        public List<NavigationItemDTO> RootItems { get; set; } = new();
        public int TotalItems { get; set; }
    }

    public class MoveNavigationItemDTO
    {
        public int ItemId { get; set; }
        public string Direction { get; set; } = "up"; // up or down
    }
}