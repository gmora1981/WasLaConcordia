using LaConcordia.DTO;
using Modelo.laconcordia.Modelo.Database;

namespace LaConcordia.Interface
{
    public interface IPasajero
    {
        Task<List<Pasajero>> GetPasajeroInfoAll();
        Task<PasajeroDTO?> GetPasajeroByCelular(string celular);
        Task InsertPasajero(Pasajero newItem);
        Task UpdatePasajero(Pasajero updItem);
        Task DeletePasajeroByCelular(string celular);
        Task<PagedResult<Pasajero>> GetPasajerosPaginados(
            int pagina,
            int pageSize,
            string? nombres = null,
            string? celular = null);
    }
}
