using Identity.Api.Interfaces;
using LaConcordia.Helpers;
using LaConcordia.Model;

namespace LaConcordia.Repository
{
    public class NavigationRepository : INavigationRepository
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api/Navigation";

        public NavigationRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<IEnumerable<NavigationItemDto>> GetAllAsync()
        {
            var httpResponse = await httpService.Get<IEnumerable<NavigationItemDto>>($"{baseURL}");

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response ?? new List<NavigationItemDto>();
        }

        public async Task<IEnumerable<NavigationItemDto>> GetActiveTreeAsync()
        {
            var httpResponse = await httpService.Get<IEnumerable<NavigationItemDto>>($"{baseURL}/tree");

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response ?? new List<NavigationItemDto>();
        }

        public async Task<NavigationItemDto?> GetByIdAsync(int id)
        {
            var httpResponse = await httpService.Get<NavigationItemDto>($"{baseURL}/{id}");

            if (!httpResponse.Success)
            {
                if (httpResponse.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response;
        }

        public async Task<NavigationItemDto> CreateAsync(NavigationItemDto dto)
        {
            var createDto = new CreateNavigationItemDto
            {
                ParentId = dto.ParentId,
                Title = dto.Title,
                Url = dto.Url,
                Icon = dto.Icon,
                Order = dto.Order,
                IsActive = dto.IsActive,
                RequiredRole = dto.RequiredRole
            };

            var httpResponse = await httpService.Post<CreateNavigationItemDto, NavigationItemDto>($"{baseURL}", createDto);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response!;
        }

        public async Task UpdateAsync(NavigationItemDto dto)
        {
            var updateDto = new UpdateNavigationItemDto
            {
                Id = dto.Id,
                ParentId = dto.ParentId,
                Title = dto.Title,
                Url = dto.Url,
                Icon = dto.Icon,
                Order = dto.Order,
                IsActive = dto.IsActive,
                RequiredRole = dto.RequiredRole
            };

            var httpResponse = await httpService.Put<UpdateNavigationItemDto, object>($"{baseURL}/{dto.Id}", updateDto);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponse = await httpService.Delete($"{baseURL}/{id}");

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        public async Task<IEnumerable<NavigationItemDto>> GetByRoleAsync(string role)
        {
            var httpResponse = await httpService.Get<IEnumerable<NavigationItemDto>>($"{baseURL}/role/{role}");

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response ?? new List<NavigationItemDto>();
        }

        public async Task<int> GetNextOrderAsync(int? parentId)
        {
            var url = parentId.HasValue
                ? $"{baseURL}/next-order/{parentId}"
                : $"{baseURL}/next-order";

            var httpResponse = await httpService.Get<NextOrderResponse>(url);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response?.NextOrder ?? 10;
        }

        public async Task ReorderItemsAsync(int? parentId)
        {
            var url = parentId.HasValue
                ? $"{baseURL}/reorder/{parentId}"
                : $"{baseURL}/reorder";

            var httpResponse = await httpService.Post<object, object>(url, new { });

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        public async Task MoveItemAsync(int itemId, string direction)
        {
            var httpResponse = await httpService.Post<object, object>($"{baseURL}/{itemId}/move/{direction}", new { });

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        // Clase auxiliar para la respuesta de next-order
        private class NextOrderResponse
        {
            public int NextOrder { get; set; }
        }
    }
}
