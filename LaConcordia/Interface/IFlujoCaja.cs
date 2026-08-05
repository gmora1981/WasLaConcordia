using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IFlujoCaja
    {
        Task<List<FlujoCajaDTO>> GetFlujoCajaInfoAll();
        Task<FlujoCajaDTO?> GetUltimoRegistro();
        Task InsertFlujoCaja(FlujoCajaDTO New);

        // Paginado
        Task<PagedResult<FlujoCajaDTO>> GetFlujoCajaPaginados(
            int pagina,
            int pageSize,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null,
            string? concepto = null);
    }
}
