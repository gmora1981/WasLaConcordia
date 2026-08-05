using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IBalance
    {
        Task<BalanceResultadoDTO> GetBalance(DateTime fechaDesde, DateTime fechaHasta);
    }
}
