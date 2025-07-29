using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IUnidad
    {
        Task<List<UnidadDTO>> GetUnidadInfoAll();

        Task<UnidadDTO> GetUnidadById(string ruc);

        Task InsertUnidad(UnidadDTO empresa);

        Task UpdateUnidad(UnidadDTO empresa);

        Task DeleteUnidadById(string ruc);

        //paginado
        Task<LaConcordia.DTO.PagedResult<UnidadDTO>> GetUnidadPaginados(int pagina,
        int pageSize,
        string? Placa = null,
        string? Idpropietario = null,
        string? Unidad1 = null,
        string? Propietario = null,
        string? Estado = null);
    }
}
