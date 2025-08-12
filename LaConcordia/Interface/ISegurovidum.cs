using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface ISegurovidum
    {
        Task<IEnumerable<SegurovidumDTO>> GetSegurovidumInfoAll();
        Task<List<SegurovidumDTO>> GetSegurovidumByCedula(string CiAfiliado);
        Task InsertSegurovidum(SegurovidumDTO nueva);
        Task UpdateSegurovidum(SegurovidumDTO actualizada);
        Task DeleteSegurovidumByCedula(int CiBeneficiario);
        Task<PagedResult<SegurovidumDTO>> GetSegurovidumPaginados(int pagina, int pageSize, string CiBeneficiario = null, string CiAfiliado = null);
        Task<PagedResult<SegurovidumDTO>> GetSegurovidumPaginadosByCedulaAfiliado(int pagina, int pageSize, string CiAfiliado);
    }
}
