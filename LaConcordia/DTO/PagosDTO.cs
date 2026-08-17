namespace LaConcordia.DTO
{
    public class DeudaSocioDTO
    {
        public string Cedula { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }

        public List<GenerarCuotaDTO> CuotasPendientes { get; set; } = new();
        public List<GenerarMultaDTO> MultasPendientes { get; set; } = new();
        public List<GenerarPlanAyudaDTO2> PlanAyudaPendientes { get; set; } = new();
        public List<GenerarPlanChoqueDTO2> PlanChoquePendientes { get; set; } = new();

        public decimal TotalCuota { get; set; }
        public decimal TotalMulta { get; set; }
        public decimal TotalPlanAyuda { get; set; }
        public decimal TotalPlanChoque { get; set; }
        public decimal TotalAPagar { get; set; }
    }

    // DTOs livianos para las filas pendientes de plan ayuda/choque en la deuda del socio
    // (nombre distinto a los DTO de generacion para no chocar con GenerarPlanAyudaRequestDTO/GenerarPlanChoqueRequestDTO).
    public class GenerarPlanAyudaDTO2
    {
        public string Beneficiario { get; set; } = null!;
        public string Cidentidad { get; set; } = null!;
        public DateOnly Fecha { get; set; }
        public string? Observacion { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Abono { get; set; }
    }

    public class GenerarPlanChoqueDTO2
    {
        public string Unidad { get; set; } = null!;
        public string Cidentidad { get; set; } = null!;
        public DateOnly Fecha { get; set; }
        public string? Observacion { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Abono { get; set; }
    }

    public class PagoCuotaRequestDTO
    {
        public string Periodo { get; set; } = null!;
        public string Semana { get; set; } = null!;
        public string Cidentidad { get; set; } = null!;
        public decimal ValorAPagar { get; set; }
        public string FormaDePago { get; set; } = null!;
        public string? Banco { get; set; }
        public string? NumComprobante { get; set; }
        public string? Detalle { get; set; }
    }

    // Tipo: "M" = Multa, "B" = Plan Ayuda, "U" = Plan Choque
    public class PagoUbmRequestDTO
    {
        public string Ubm { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Cidentidad { get; set; } = null!;
        public decimal ValorAPagar { get; set; }
        public string FormaDePago { get; set; } = null!;
        public string? Banco { get; set; }
        public string? NumComprobante { get; set; }
        public string? Detalle { get; set; }
    }

    public class DetallePagoMonitoriaDTO
    {
        public string Periodo { get; set; } = null!;
        public string Semana { get; set; } = null!;
        public DateTime Fechapago { get; set; }
        public decimal? Valorpagado { get; set; }
        public string? Formadepago { get; set; }
        public string? Detalle { get; set; }
    }
}
