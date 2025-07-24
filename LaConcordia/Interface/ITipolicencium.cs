using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface ITipolicencium
    {
        Task<List<TipolicenciumDTO>> GetTipolicenciaInfoAll();

        Task<TipolicenciumDTO> GetTipolicenciaById(int idTipoLicencia);

        Task InsertTipolicencia(TipolicenciumDTO nuevaDto);

        Task UpdateTipolicencia(TipolicenciumDTO actualizada);

        Task DeleteTipolicencia(int idTipoLicencia);

        //paginado
        Task<LaConcordia.DTO.PagedResult<TipolicenciumDTO>> GetTipoLicenciumPaginados
            (int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
