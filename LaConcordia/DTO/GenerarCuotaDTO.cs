namespace LaConcordia.DTO
{
    public class PendienteCuotaDTO
    {
        public string Cedula { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public decimal? Cuotaf { get; set; }
    }

    public class GenerarCuotaResultadoDTO
    {
        public int TotalGenerados { get; set; }
        public List<string> CedulasGeneradas { get; set; } = new();
    }
}
