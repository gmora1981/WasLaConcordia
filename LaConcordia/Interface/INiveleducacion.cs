using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface INiveleducacion
    {
        Task<List<NiveleducacionDTO>> GetNiveleducacionAll();
        Task<NiveleducacionDTO> GetNiveleducacionById(int id);
        Task InsertNiveleducacion(NiveleducacionDTO niveleducacion);
        Task UpdateNiveleducacion(NiveleducacionDTO niveleducacion);
        Task DeleteNiveleducacionById(int id);
        // Paginado
        Task<LaConcordia.DTO.PagedResult<NiveleducacionDTO>> GetNiveleducacionPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);
    }
}
