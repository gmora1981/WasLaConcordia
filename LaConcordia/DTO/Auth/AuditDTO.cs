namespace LaConcordia.DTO.Auth
{
    public class AuditLogDTO
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
    }

    public class AuditFilterDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? UserId { get; set; }
        public string? Action { get; set; }
        public string? EntityType { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class AuditSummaryDTO
    {
        public Dictionary<string, int> ActionCounts { get; set; } = new();
        public Dictionary<string, int> UserActivityCounts { get; set; } = new();
        public List<string> MostActiveUsers { get; set; } = new();
        public List<string> RecentActions { get; set; } = new();
    }
}