using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IFichaobservacione
    {
        Task<IEnumerable<FichaobservacioneDTO>> GetFichaObservacioneInfoAll();
        Task<List<FichaobservacioneDTO>> GetFichaObservacioneByCedula(string cedula);
        Task InsertFichaObservacione(FichaobservacioneDTO nueva);
        Task UpdateFichaObservacione(FichaobservacioneDTO actualizada);
        Task DeleteFichaObservacioneByCedula(int idfichaobs);
        Task<PagedResult<FichaobservacioneDTO>> GetFichaObservacionePaginados(int pagina, int pageSize, string unidad = null, string cedula = null);
        Task<PagedResult<FichaobservacioneDTO>> GetFichaObservacionePaginadosByCedula(int pagina, int pageSize, string fkCedula);
    }
}
