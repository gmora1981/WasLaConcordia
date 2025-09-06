namespace LaConcordia.Model
{
    // Clases para manejo de permisos
    public class NavigationRolePermission
    {
        public string RoleId { get; set; } = "";
        public string RoleName { get; set; } = "";
        public int UsersCount { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
