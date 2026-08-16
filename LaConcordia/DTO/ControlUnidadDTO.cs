namespace LaConcordia.DTO
{
    public class UnidadServicioDTO
    {
        public string Fkunidad { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
    }

    public class MoverUnidadRequestDTO
    {
        public string Unidad { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Direccion { get; set; } = null!;
    }

    public class ControlUnidadMovimientoDTO
    {
        public DateTime Fecharegistro { get; set; }
        public string? Turno { get; set; }
        public string Unidad { get; set; } = null!;
        public string? Ciconductor { get; set; }
        public string? Conductor { get; set; }
        public string Estado { get; set; } = null!;
        public string? Monitora { get; set; }
    }
}
