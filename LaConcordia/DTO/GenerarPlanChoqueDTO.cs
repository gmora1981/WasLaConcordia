namespace LaConcordia.DTO
{
    public class GenerarPlanChoqueRequestDTO
    {
        public string Unidad { get; set; } = null!;
        public string? Observacion { get; set; }
        public decimal Valor { get; set; }
    }
}
