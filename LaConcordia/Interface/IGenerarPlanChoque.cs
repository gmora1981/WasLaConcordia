using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGenerarPlanChoque
    {
        Task<bool> YaFueGenerado(string unidad);
        Task<GenerarPlanResultadoDTO> GenerarPlanChoque(GenerarPlanChoqueRequestDTO request);
    }
}
