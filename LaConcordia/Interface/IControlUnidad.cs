using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IControlUnidad
    {
        Task<List<UnidadServicioDTO>> GetFichaPersonalPorServicio(string estadoServicio);
        Task MoverUnidad(MoverUnidadRequestDTO request);
        Task<byte[]> ExportarPdf(string? turno);
    }
}
