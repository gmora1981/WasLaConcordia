namespace LaConcordia.DTO
{
    public class SolicitudCarreraDTO
    {
        public int Idsolicitud { get; set; }
        public string Ruc { get; set; } = null!;
        public string? RazonSocial { get; set; }
        public string Celular { get; set; } = null!;
        public string? Empleado { get; set; }
        public decimal Origenlat { get; set; }
        public decimal Origenlog { get; set; }
        public decimal? Destinolat { get; set; }
        public decimal? Destinolog { get; set; }
        public string? Observacion { get; set; }
        public DateTime Fechasolicitud { get; set; }
        public string Estado { get; set; } = null!;
    }

    public class CrearSolicitudCarreraRequestDTO
    {
        public string Celular { get; set; } = null!;
        public string? Empleado { get; set; }
        public decimal Origenlat { get; set; }
        public decimal Origenlog { get; set; }
        public decimal? Destinolat { get; set; }
        public decimal? Destinolog { get; set; }
        public string? Observacion { get; set; }
    }
}
