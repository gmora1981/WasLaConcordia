using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGenerarPlanAyuda
    {
        Task<List<BeneficiarioDTO>> GetBeneficiariosPorAfiliado(string ciAfiliado);
        Task<bool> YaFueGenerado(string beneficiario);
        Task<GenerarPlanResultadoDTO> GenerarPlanAyuda(GenerarPlanAyudaRequestDTO request);
    }
}
