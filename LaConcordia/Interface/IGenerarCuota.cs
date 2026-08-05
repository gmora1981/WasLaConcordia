using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGenerarCuota
    {
        Task<List<PendienteCuotaDTO>> GetPendientesPorPeriodo(string periodo, string semana);
        Task<GenerarCuotaResultadoDTO> GenerarCuotaSemanal(string periodo, string semana);
    }
}
