using LaConcordia.Model;
using System.Linq.Expressions;

namespace Identity.Api.Interfaces
{
    public interface  INavigationRepository
    {
        Task<IEnumerable<NavigationItemDto>> GetAllAsync();
        Task<IEnumerable<NavigationItemDto>> GetActiveTreeAsync();
        Task<NavigationItemDto?> GetByIdAsync(int id);
        Task<NavigationItemDto> CreateAsync(NavigationItemDto dto);
        Task UpdateAsync(NavigationItemDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<NavigationItemDto>> GetByRoleAsync(string role);
        Task<int> GetNextOrderAsync(int? parentId);
        Task ReorderItemsAsync(int? parentId);
        Task MoveItemAsync(int itemId, string direction);
    }
}
