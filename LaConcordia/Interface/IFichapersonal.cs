using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IFichapersonal
    {
        Task<List<FichapersonalDTO>> GetFichaPersonalInfoAll();

        Task<FichapersonalDTO> GetFichaPersonalById(string cedula);

        Task<FichapersonalDTO> GetFichaPersonalByCorreo(string correo);

        Task InsertFichaPersonal(FichapersonalDTO New);

        Task UpdateFichaPersonal(FichapersonalDTO UpdItem);

        Task DeleteFichaPersonalById(string cedula);

        //paginado
        Task<LaConcordia.DTO.PagedResult<FichapersonalDTO>> GetFichaPersonalPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null);

        //exportar 
        Task<byte[]> ExportarFichaCompleta(ExportFichaDTO exportData);

        //exportar imagenes
        // Descargar PDF de imágenes del chofer
        Task<byte[]> DescargarImagenesChoferPdf(string cedula);

        // Subir imagen del chofer frontal y trasera
        Task SubirImagenChoferAsync(Stream contenido, string nombreArchivo, string cedulaChofer, string tipoDocumento);

        // Subir imagenes
        Task SubirImagenLicenciaAsync(Stream contenido, string nombreArchivo, string cedula);
        Task SubirImagenMatriculaAsync(Stream contenido, string nombreArchivo, string cedula);
        Task SubirImagenVehiculoAsync(Stream contenido, string nombreArchivo, string cedula);



        // Obtener estado de imágenes por cédula
        Task<EstadoImagenDTO> ObtenerEstadoImagenesAsync(string cedula);


        // Eliminar imagen por cédula
        Task EliminarImagenChoferAsync(string cedulaChofer);

    }
}
