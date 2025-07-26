namespace LaConcordia.Interface
{
    public interface INacionalidad
    {
        Task<List<DTO.NacionalidadDTO>> GetNacionalidadesAll();
        Task<DTO.NacionalidadDTO> GetNacionalidadById(int idnacionalidad);
        Task InsertNacionalidad(DTO.NacionalidadDTO nacionalidad);
        Task UpdateNacionalidad(DTO.NacionalidadDTO nacionalidad);
        Task DeleteNacionalidadById(int idnacionalidad);
        // Paginado
        Task<DTO.PagedResult<DTO.NacionalidadDTO>> GetNacionalidadesPaginados(
            int pagina, int pageSize, string? nacionalidad = null, string? estado = null);
    }
}
