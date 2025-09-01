using LaConcordia.DTO;
using LaConcordia.Model;
using LaConcordia.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Interface
{
    public interface INavigationManagement
    {
        // CRUD de elementos de navegación
        Task<List<NavigationItemDto>> GetAllNavigationItems();
        Task<PagedResult<NavigationItemDto>> GetNavigationItemsPaginados(int pagina, int pageSize, string? filtro = null);
        Task<NavigationItemDto> GetNavigationItemById(int id);
        Task<NavigationItemDto> CreateNavigationItem(CreateNavigationItemDto item);
        Task UpdateNavigationItem(UpdateNavigationItemDto item);
        Task DeleteNavigationItem(int id);

        // Gestión de jerarquía
        Task<List<NavigationItemDto>> GetNavigationTree();
        Task<List<NavigationItemDto>> GetChildrenItems(int parentId);
        Task MoveItem(int itemId, string direction);
        Task ReorderItems(int? parentId);
        Task<int> GetNextOrder(int? parentId);

        // Permisos de navegación
        Task<List<NavigationItemDto>> GetNavigationByRole(string roleName);
        Task<List<NavigationItemDto>> GetNavigationByUser(string userId);
        Task<NavigationMenuDto> GetUserNavigationMenu(string userId);

        // Validaciones
        Task<bool> HasChildren(int itemId);
        Task<bool> CanDelete(int itemId);
        Task<bool> IsValidParent(int itemId, int? parentId);
    }
}