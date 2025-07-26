using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface ICargo
    {
        Task<List<CargoDTO>> GetCargoInfoAll();
        Task<CargoDTO> GetCargoById(int id);
        Task InsertCargo(CargoDTO New);
        Task UpdateCargo(CargoDTO UpdItem);
        Task DeleteCargoById(int id);
        // Paginado
        Task<LaConcordia.DTO.PagedResult<CargoDTO>> GetCargosPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);

    }
}
