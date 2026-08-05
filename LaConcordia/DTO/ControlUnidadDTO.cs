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
}
