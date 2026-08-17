using LaConcordia.DTO;
using Modelo.laconcordia.Modelo.Database;
using System;

namespace LaConcordia.Interface
{
    public interface IPedido
    {
        Task InsertPedido(Pedido newItem);
        Task UpdatePedido(Pedido updItem);
        Task<PagedResult<PedidoDTO>> GetPedidosPaginados(
            int pagina,
            int pageSize,
            string? celular = null,
            string? unidad = null,
            string? estado = null,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null);

        Task<ConductorInfoDTO?> GetConductorPorUnidad(string unidad);

        Task<PrecioKmDTO?> GetPrecioKmHistorico(string celular, decimal origenLat, decimal origenLog, decimal destinoLat, decimal destinoLog);

        Task<List<PedidoDTO>> GetPedidosConDestinoPendiente();

        Task GuardarDireccion(GuardarDireccionRequestDTO request);

        Task<List<PedidosPorUsuarioDTO>> GetCantidadPedidosPorUsuario(DateTime desde, DateTime hasta);
        Task<List<PedidosPorUnidadDTO>> GetTopUnidadesConMasCarreras(DateTime desde, DateTime hasta);

        Task<List<string>> GetUsuariosDisponibles();
        Task<List<string>> GetUnidadesConPedidos();
        Task<List<PedidoOperadoraDTO>> GetPedidosPorOperadora(string? usuario, DateTime desde, DateTime hasta, string? unidad = null);
        Task<byte[]> ExportarReporteSolicitudCarreraPdf(string? usuario, DateTime desde, DateTime hasta, string? unidad = null);
    }
}
