namespace LaConcordia.Model
{
    public class RoleReportItem
    {
        public string RoleId { get; set; } = "";
        public string RoleName { get; set; } = "";
        public int UserCount { get; set; }
        public int ViewCount { get; set; }
        public int CreateCount { get; set; }
        public int EditCount { get; set; }
        public int DeleteCount { get; set; }
    }
}
