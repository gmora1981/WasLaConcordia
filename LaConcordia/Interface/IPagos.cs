using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IPagos
    {
        Task<DeudaSocioDTO?> GetDeudaSocio(string cedula);
        Task<bool> ExisteComprobante(string banco, string numComprobante);
        Task PagarCuota(PagoCuotaRequestDTO request);
        Task PagarUbm(PagoUbmRequestDTO request);

        Task<List<DetallePagoMonitoriaDTO>> GetDetallePagosPorUnidad(string unidad, DateTime desde, DateTime hasta);
        Task<byte[]> ExportarReporteDetallePagosPdf(string unidad, DateTime desde, DateTime hasta);

        Task<ResumenMonitoriaDTO> GetResumenMonitoria(DateTime desde, DateTime hasta);
    }
}
