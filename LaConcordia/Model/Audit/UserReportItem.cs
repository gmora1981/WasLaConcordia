using LaConcordia.Repository;

namespace LaConcordia.Model
{
    // Classes for report items
    public class UserReportItem
    {
        public string UserId { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new();
        public int PermissionCount { get; set; }
        public List<NavigationPermissionDto> Permissions { get; set; } = new();
    }
}
