namespace LaConcordia.Model
{
    // Clase auxiliar para elementos de navegación con selección
    public class BulkNavigationItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Icon { get; set; }
        public string? RequiredRole { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
