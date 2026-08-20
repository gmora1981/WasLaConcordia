using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IOrdenPago
    {
        Task<List<PedidoVoucherDTO>> GetPedidosPendientesVoucher(string? ruc = null);
        Task<string?> GetDireccionTexto(string celular, decimal lat, decimal lng);
        Task<decimal> GetSaldoCajaActual();
        Task<OrdenPagoResultadoDTO> GenerarOrdenPago(GenerarOrdenPagoRequestDTO request);

        Task<List<OrdenPagoResumenDTO>> GetOrdenPagoPorEmpresa(string ruc, DateTime? hasta = null);

        // "Modificar Datos": corrige Precio/Recorrido/Empleado sin generar voucher.
        Task ActualizarDatosPedido(ActualizarDatosPedidoRequestDTO request);

        Task<byte[]> ExportarFacturacionPdf(string ruc, string razonSocial, DateTime? hasta);

        Task<List<ReporteVoucherPagarDTO>> GetVouchersPendientesPorUnidad(string? unidad, DateTime desde, DateTime hasta);
        Task<byte[]> ExportarReporteVoucherPagarPdf(string? unidad, DateTime desde, DateTime hasta);

        Task<ResumenVoucherDTO> GetResumenVouchers(DateTime desde, DateTime hasta);

        // Portal de Empresas.
        Task<string?> GetMiRuc();
        Task<ResumenVoucherDTO> GetResumenVouchersEmpresa(DateTime desde, DateTime hasta);
    }
}
