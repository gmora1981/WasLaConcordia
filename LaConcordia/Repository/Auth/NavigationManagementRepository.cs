using LaConcordia.Interface;
using LaConcordia.Model;
using LaConcordia.DTO;
using LaConcordia.Repository;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public class NavigationManagementRepository : INavigationManagement
    {
        private readonly HttpClient _httpClient;
        private readonly IPermissionService _permissionService;

        public NavigationManagementRepository(HttpClient httpClient, IPermissionService permissionService)
        {
            _httpClient = httpClient;
            _permissionService = permissionService;
        }

        public async Task<List<NavigationItemDto>> GetAllNavigationItems()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<NavigationItemDto>>("api/Navigation")
                    ?? new List<NavigationItemDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener elementos de navegación", ex);
            }
        }

        public async Task<PagedResult<NavigationItemDto>> GetNavigationItemsPaginados(int pagina, int pageSize, string? filtro = null)
        {
            var url = $"api/Navigation/GetNavigationPaginados?pagina={pagina}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(filtro))
                url += $"&filtro={Uri.EscapeDataString(filtro)}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<NavigationItemDto>>(url)
                    ?? new PagedResult<NavigationItemDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener navegación paginada", ex);
            }
        }

        public async Task<NavigationItemDto> GetNavigationItemById(int id)
        {
            try
            {
                var item = await _httpClient.GetFromJsonAsync<NavigationItemDto>($"api/Navigation/{id}");
                return item ?? throw new Exception("Elemento de navegación no encontrado");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener elemento de navegación con ID {id}", ex);
            }
        }

        public async Task<NavigationItemDto> CreateNavigationItem(CreateNavigationItemDto item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Navigation", item);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear elemento de navegación: {errorContent}");
                }

                var createdItem = await response.Content.ReadFromJsonAsync<NavigationItemDto>();
                return createdItem ?? throw new Exception("Error al obtener el elemento creado");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al crear elemento de navegación", ex);
            }
        }

        public async Task UpdateNavigationItem(UpdateNavigationItemDto item)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Navigation/{item.Id}", item);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar elemento de navegación: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al actualizar elemento de navegación", ex);
            }
        }

        public async Task DeleteNavigationItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Navigation/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    if (errorContent.Contains("elementos hijos"))
                        throw new Exception("No se puede eliminar porque tiene elementos hijos");

                    throw new Exception($"Error al eliminar elemento de navegación: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al eliminar elemento de navegación", ex);
            }
        }

        public async Task<List<NavigationItemDto>> GetNavigationTree()
        {
            try
            {
                var items = await GetAllNavigationItems();
                return BuildNavigationTree(items);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al construir árbol de navegación", ex);
            }
        }

        public async Task<List<NavigationItemDto>> GetChildrenItems(int parentId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<NavigationItemDto>>($"api/Navigation/children/{parentId}")
                    ?? new List<NavigationItemDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener elementos hijos de {parentId}", ex);
            }
        }

        public async Task MoveItem(int itemId, string direction)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/Navigation/{itemId}/move/{direction}", null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al mover elemento: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al mover elemento", ex);
            }
        }

        public async Task ReorderItems(int? parentId)
        {
            try
            {
                var url = parentId.HasValue
                    ? $"api/Navigation/reorder/{parentId}"
                    : "api/Navigation/reorder";

                var response = await _httpClient.PostAsync(url, null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al reordenar elementos: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al reordenar elementos", ex);
            }
        }

        public async Task<int> GetNextOrder(int? parentId)
        {
            try
            {
                var url = parentId.HasValue
                    ? $"api/Navigation/next-order/{parentId}"
                    : "api/Navigation/next-order";

                var response = await _httpClient.GetFromJsonAsync<NextOrderResponse>(url);
                return response?.NextOrder ?? 10;
            }
            catch
            {
                return 10; // Valor por defecto
            }
        }

        public async Task<List<NavigationItemDto>> GetNavigationByRole(string roleName)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<NavigationItemDto>>($"api/Navigation/role/{roleName}")
                    ?? new List<NavigationItemDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener navegación para rol {roleName}", ex);
            }
        }

        public async Task<List<NavigationItemDto>> GetNavigationByUser(string userId)
        {
            try
            {
                var permissions = await _permissionService.GetEffectivePermissionsAsync(userId);

                // Filtrar solo los elementos con permisos de visualización
                return permissions
                    .Where(p => p.CanView)
                    .Select(p => new NavigationItemDto
                    {
                        Id = p.NavigationItemId,
                        Title = p.NavigationItemTitle,
                        Url = p.NavigationItemUrl,
                        Icon = p.NavigationItemIcon,
                        ParentId = p.ParentId,
                        IsActive = true,
                        Children = new List<NavigationItemDto>()
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener navegación para usuario {userId}", ex);
            }
        }

        public async Task<NavigationMenuDto> GetUserNavigationMenu(string userId)
        {
            try
            {
                return await _permissionService.GetUserNavigationMenuAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener menú de navegación del usuario {userId}", ex);
            }
        }

        public async Task<bool> HasChildren(int itemId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Navigation/{itemId}/has-children");

                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CanDelete(int itemId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Navigation/{itemId}/can-delete");

                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsValidParent(int itemId, int? parentId)
        {
            try
            {
                if (!parentId.HasValue)
                    return true;

                var response = await _httpClient.GetAsync($"api/Navigation/{itemId}/is-valid-parent/{parentId}");

                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }

        // Métodos auxiliares privados
        private List<NavigationItemDto> BuildNavigationTree(List<NavigationItemDto> items)
        {
            var lookup = items.ToLookup(i => i.ParentId);

            var rootItems = items.Where(i => i.ParentId == null).ToList();

            foreach (var root in rootItems)
            {
                BuildChildren(root, lookup);
            }

            return rootItems;
        }

        private void BuildChildren(NavigationItemDto parent, ILookup<int?, NavigationItemDto> lookup)
        {
            parent.Children = lookup[parent.Id].ToList();

            foreach (var child in parent.Children)
            {
                BuildChildren(child, lookup);
            }
        }

        // Clase auxiliar para la respuesta de next-order
        private class NextOrderResponse
        {
            public int NextOrder { get; set; }
        }
    }
}