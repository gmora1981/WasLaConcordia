using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IFichapersonal
    {
        Task<List<FichapersonalDTO>> GetFichaPersonalInfoAll();

        Task<FichapersonalDTO> GetFichaPersonalById(string cedula);

        Task InsertFichaPersonal(FichapersonalDTO New);

        Task UpdateFichaPersonal(FichapersonalDTO UpdItem);

        Task DeleteFichaPersonalById(string cedula);

        //paginado
        Task<LaConcordia.DTO.PagedResult<FichapersonalDTO>> GetFichaPersonalPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);

        //exportar 
        Task<byte[]> ExportarFichaCompleta(ExportFichaDTO exportData);

    }
}
