using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IEmpresa
    {
        //http://localhost:5191/api/Accounts/Login 
        Task<List<EmpresaDTO>> GetEmpresaInfoAll();

        Task<EmpresaDTO> GetEmpresaByRuc(string ruc);

        Task InsertEmpresa(EmpresaDTO empresa);

        Task UpdateEmpresa(EmpresaDTO empresa);

        Task DeleteEmpresaByRuc(string ruc);

        //paginado
        Task<LaConcordia.DTO.PagedResult<EmpresaDTO>> GetEmpresasPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

    }
}
