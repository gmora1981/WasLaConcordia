using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGenerarMulta
    {
        Task<List<GenerarMultaDTO>> GetMultasPorSocio(string cidentidad);
        Task<GenerarMultaDTO> InsertGenerarMulta(GenerarMultaDTO nueva);
    }
}
