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
    }
}
