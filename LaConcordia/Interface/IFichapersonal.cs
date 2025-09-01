using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IFichapersonal
    {
        Task<List<FichapersonalDTO>> GetFichaPersonalInfoAll();

        Task<FichapersonalDTO> GetFichaPersonalById(string cedula);

        Task InsertFichaPersonal(FichapersonalDTO New);

        Task UpdateFichaPersonal(FichapersonalDTO UpdItem);

        Task DeleteFichaPersonalById(string cedula);

        //paginado
        Task<LaConcordia.DTO.PagedResult<FichapersonalDTO>> GetFichaPersonalPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);

        //exportar 
        Task<byte[]> ExportarFichaCompleta(ExportFichaDTO exportData);

        // Subir imagen del chofer frontal y trasera
        Task SubirImagenChoferAsync(Stream contenido, string nombreArchivo, string cedulaChofer, string tipoDocumento);

        // Subir imagen de chofer (Licencia, Matrícula o Vehículo)
        Task SubirImagenChoferLMV (Stream contenido, string nombreArchivo, string cedulaChofer, string tipoCarpeta);

        // Buscar imagen por cédula
        Task<string?> BuscarImagenChoferAsync(string cedulaChofer);

        // Eliminar imagen por cédula
        Task EliminarImagenChoferAsync(string cedulaChofer);

    }
}
