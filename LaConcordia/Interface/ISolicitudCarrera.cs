using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface ISolicitudCarrera
    {
        Task CrearSolicitud(CrearSolicitudCarreraRequestDTO request);
        Task<List<SolicitudCarreraDTO>> GetMisSolicitudes();
        Task<List<SolicitudCarreraDTO>> GetSolicitudesPendientes();
        Task MarcarConvertida(int idsolicitud);
    }
}
