namespace LaConcordia.Interface
{
    public interface IDuenopuesto
    {
        Task<List<DTO.DuenopuestoDTO>> DuenopuestoInfoAll();
        Task<DTO.DuenopuestoDTO> GetDuenopuestoByCedula(string cedula);
        Task InsertDuenopuesto(DTO.DuenopuestoDTO duenopuesto);
        Task UpdateDuenopuesto(DTO.DuenopuestoDTO duenopuesto);
        Task DeleteDuenopuestoByCedula(string cedula);
        // Paginado
        Task<DTO.PagedResult<DTO.DuenopuestoDTO>> GetDuenopuestosPaginados(int pagina,
            int pageSize,
            string? cedula = null,
            string? nombre = null,
            string? apellidos = null,
            string? estado = null);
    }
}
