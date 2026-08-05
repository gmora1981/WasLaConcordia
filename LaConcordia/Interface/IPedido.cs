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
    }
}
