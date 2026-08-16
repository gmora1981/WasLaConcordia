using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IControlUnidad
    {
        Task<List<UnidadServicioDTO>> GetFichaPersonalPorServicio(string estadoServicio);
        Task MoverUnidad(MoverUnidadRequestDTO request);
        Task<byte[]> ExportarPdf(string? turno);

        // "Reporte de Ingreso y Salida" por operadora y rango de fechas.
        Task<List<string>> GetMonitorasDisponibles();
        Task<List<ControlUnidadMovimientoDTO>> GetMovimientosPorRango(DateTime desde, DateTime hasta, string? monitora);
        Task<byte[]> ExportarReporteIngresoSalidaPdf(DateTime desde, DateTime hasta, string? monitora);
    }
}
