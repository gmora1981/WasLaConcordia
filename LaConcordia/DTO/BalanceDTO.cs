namespace LaConcordia.DTO
{
    public class BalanceItemDTO
    {
        public string FormaDePago { get; set; } = null!;
        public decimal Valor { get; set; }
        public int Registros { get; set; }
    }

    public class BalanceResultadoDTO
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public List<BalanceItemDTO> Items { get; set; } = new();
        public decimal Total { get; set; }
    }
}
