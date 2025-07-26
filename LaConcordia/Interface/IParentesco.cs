namespace LaConcordia.Interface
{
    public interface IParentesco
    {
        Task<List<DTO.ParentescoDTO>> ParentescoInfoAll();
        Task<DTO.ParentescoDTO> GetParentescoById(int id);
        Task InsertParentesco(DTO.ParentescoDTO parentesco);
        Task UpdateParentesco(DTO.ParentescoDTO parentesco);
        Task DeleteParentescoById(int id);
        // Paginado
        Task<DTO.PagedResult<DTO.ParentescoDTO>> GetParentescosPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);
    }
}
