using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IPagos
    {
        Task<DeudaSocioDTO?> GetDeudaSocio(string cedula);
        Task<bool> ExisteComprobante(string banco, string numComprobante);
        Task PagarCuota(PagoCuotaRequestDTO request);
        Task PagarUbm(PagoUbmRequestDTO request);
    }
}
