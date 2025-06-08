namespace LaConcordia.Model
{
    public class CreateNavigationItemDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RequiredRole { get; set; }
    }
}
