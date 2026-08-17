using LaConcordia.DTO;
using LaConcordia.Interface;
using Modelo.laconcordia.Modelo.Database;
using System;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class PedidoRepository : IPedido
    {
        private readonly HttpClient _httpClient;

        public PedidoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task InsertPedido(Pedido newItem)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pedido/InsertPedido", newItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Pedido: {errorContent}");
            }
        }

        public async Task UpdatePedido(Pedido updItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Pedido/UpdatePedido", updItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Pedido: {errorContent}");
            }
        }

        public async Task<PagedResult<PedidoDTO>> GetPedidosPaginados(
            int pagina,
            int pageSize,
            string? celular = null,
            string? unidad = null,
            string? estado = null,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null)
        {
            var url = $"api/Pedido/GetPedidoPaginados?pagina={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(celular))
                url += $"&celular={Uri.EscapeDataString(celular)}";
            if (!string.IsNullOrEmpty(unidad))
                url += $"&unidad={Uri.EscapeDataString(unidad)}";
            if (!string.IsNullOrEmpty(estado))
                url += $"&estado={Uri.EscapeDataString(estado)}";
            if (fechaDesde.HasValue)
                url += $"&fechaDesde={fechaDesde.Value.ToString("o")}";
            if (fechaHasta.HasValue)
                url += $"&fechaHasta={fechaHasta.Value.ToString("o")}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<PedidoDTO>>(url) ?? new PagedResult<PedidoDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los Pedidos paginados.", ex);
            }
        }

        public async Task<ConductorInfoDTO?> GetConductorPorUnidad(string unidad)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ConductorInfoDTO>($"api/Pedido/GetConductorPorUnidad/{unidad}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<PrecioKmDTO?> GetPrecioKmHistorico(string celular, decimal origenLat, decimal origenLog, decimal destinoLat, decimal destinoLog)
        {
            var url = $"api/Pedido/GetPrecioKmHistorico?celular={Uri.EscapeDataString(celular)}&origenLat={origenLat}&origenLog={origenLog}&destinoLat={destinoLat}&destinoLog={destinoLog}";
            return await _httpClient.GetFromJsonAsync<PrecioKmDTO?>(url);
        }

        public async Task<List<PedidoDTO>> GetPedidosConDestinoPendiente()
        {
            return await _httpClient.GetFromJsonAsync<List<PedidoDTO>>("api/Pedido/GetPedidosConDestinoPendiente") ?? new List<PedidoDTO>();
        }

        public async Task GuardarDireccion(GuardarDireccionRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pedido/GuardarDireccion", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al guardar la dirección: {errorContent}");
            }
        }

        public async Task<List<PedidosPorUsuarioDTO>> GetCantidadPedidosPorUsuario(DateTime desde, DateTime hasta)
        {
            var url = $"api/Pedido/GetCantidadPedidosPorUsuario?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            return await _httpClient.GetFromJsonAsync<List<PedidosPorUsuarioDTO>>(url) ?? new List<PedidosPorUsuarioDTO>();
        }

        public async Task<List<string>> GetUsuariosDisponibles()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/Pedido/GetUsuariosDisponibles") ?? new List<string>();
        }

        public async Task<List<string>> GetUnidadesConPedidos()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/Pedido/GetUnidadesConPedidos") ?? new List<string>();
        }

        public async Task<List<PedidoOperadoraDTO>> GetPedidosPorOperadora(string? usuario, DateTime desde, DateTime hasta, string? unidad = null)
        {
            var url = $"api/Pedido/GetPedidosPorOperadora?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(usuario))
                url += $"&usuario={Uri.EscapeDataString(usuario)}";
            if (!string.IsNullOrEmpty(unidad))
                url += $"&unidad={Uri.EscapeDataString(unidad)}";

            return await _httpClient.GetFromJsonAsync<List<PedidoOperadoraDTO>>(url) ?? new List<PedidoOperadoraDTO>();
        }

        public async Task<byte[]> ExportarReporteSolicitudCarreraPdf(string? usuario, DateTime desde, DateTime hasta, string? unidad = null)
        {
            var url = $"api/Pedido/ExportarReporteSolicitudCarreraPdf?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(usuario))
                url += $"&usuario={Uri.EscapeDataString(usuario)}";
            if (!string.IsNullOrEmpty(unidad))
                url += $"&unidad={Uri.EscapeDataString(unidad)}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al exportar el reporte: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
