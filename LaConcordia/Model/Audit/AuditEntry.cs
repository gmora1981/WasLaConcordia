namespace LaConcordia.Model
{

    // Clase para entradas de auditoría
    public class AuditEntry
    {
        public DateTime Date { get; set; }
        public string Action { get; set; } = "";
        public string Description { get; set; } = "";
        public string EntityType { get; set; } = "";
        public string EntityName { get; set; } = "";
        public string PerformedBy { get; set; } = "";
        public string NavigationItem { get; set; } = "";
        public string Type { get; set; } = ""; // "grant", "revoke", "modify"
        public List<string> Changes { get; set; } = new();
    }
}
