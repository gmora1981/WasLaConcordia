using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IEstadoCivil
    {
        Task<List<EstadoCivilDTO>> GetEstadoCivilAll();    

        Task<EstadoCivilDTO> GetEstadoCivilById(int id);

        Task InsertEstadoCivil(EstadoCivilDTO estadoCivil);

        Task UpdateEstadoCivil(EstadoCivilDTO estadoCivil);

        Task DeleteEstadoCivilById(int id);

        // Paginado
        Task<LaConcordia.DTO.PagedResult<EstadoCivilDTO>> GetEstadoCivilPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);


    }
}
