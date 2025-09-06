namespace LaConcordia.Model
{
    public class NavigationReportItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Icon { get; set; }
        public string? RequiredRole { get; set; }
        public int UsersWithAccess { get; set; }
        public int RolesWithAccess { get; set; }
    }
}
