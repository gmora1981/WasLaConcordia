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
    }
}
