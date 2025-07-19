// PermissionService.cs - Implementación del servicio de permisos para el cliente Blazor
using LaConcordia.Helpers;
using LaConcordia.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace LaConcordia.Repository
{
    public class PermissionService : IPermissionService
    {
        private readonly IHttpService httpService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly string baseURL = "api/permissions";

        public PermissionService(IHttpService httpService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpService = httpService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        #region User Permissions

        public async Task<UserPermissionsDto> GetUserPermissionsAsync(string userId)
        {
            var response = await httpService.Get<UserPermissionsDto>($"{baseURL}/user/{userId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<NavigationPermissionDto> GetUserPermissionForItemAsync(string userId, int navigationItemId)
        {
            var response = await httpService.Get<NavigationPermissionDto>($"{baseURL}/user/{userId}/item/{navigationItemId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task UpdateUserPermissionAsync(UpdateUserPermissionDto dto)
        {
            var response = await httpService.Post<UpdateUserPermissionDto, object>($"{baseURL}/user/update", dto);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task RemoveUserPermissionAsync(string userId, int navigationItemId)
        {
            var response = await httpService.Delete($"{baseURL}/user/{userId}/item/{navigationItemId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task AssignBulkUserPermissionsAsync(string userId, BulkPermissionAssignmentDto dto)
        {
            var response = await httpService.Post<BulkPermissionAssignmentDto, object>($"{baseURL}/user/{userId}/bulk-assign", dto);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        #endregion

        #region Role Permissions

        public async Task<RolePermissionsDto> GetRolePermissionsAsync(string roleId)
        {
            var response = await httpService.Get<RolePermissionsDto>($"{baseURL}/role/{roleId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<NavigationPermissionDto> GetRolePermissionForItemAsync(string roleId, int navigationItemId)
        {
            var response = await httpService.Get<NavigationPermissionDto>($"{baseURL}/role/{roleId}/item/{navigationItemId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task UpdateRolePermissionAsync(UpdateRolePermissionDto dto)
        {
            var response = await httpService.Post<UpdateRolePermissionDto, object>($"{baseURL}/role/update", dto);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task RemoveRolePermissionAsync(string roleId, int navigationItemId)
        {
            var response = await httpService.Delete($"{baseURL}/role/{roleId}/item/{navigationItemId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task AssignBulkRolePermissionsAsync(string roleId, BulkPermissionAssignmentDto dto)
        {
            var response = await httpService.Post<BulkPermissionAssignmentDto, object>($"{baseURL}/role/{roleId}/bulk-assign", dto);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        #endregion

        #region Effective Permissions

        public async Task<List<NavigationPermissionDto>> GetEffectivePermissionsAsync(string userId)
        {
            var response = await httpService.Get<List<NavigationPermissionDto>>($"{baseURL}/user/{userId}/effective");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<NavigationMenuDto> GetUserNavigationMenuAsync(string userId)
        {
            var response = await httpService.Get<NavigationMenuDto>($"{baseURL}/my-navigation-menu");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<bool> CheckPermissionAsync(CheckPermissionDto dto)
        {
            var response = await httpService.Post<CheckPermissionDto, bool>($"{baseURL}/check", dto);
            if (!response.Success)
            {
                return false; // Por defecto no tiene permiso si hay error
            }
            return response.Response;
        }

        #endregion

        #region Advanced Management

        public async Task<bool> CopyUserPermissionsAsync(string sourceUserId, string targetUserId)
        {
            var dto = new CopyPermissionsDto
            {
                SourceId = sourceUserId,
                TargetId = targetUserId,
                EntityType = "User"
            };

            var response = await httpService.Post<CopyPermissionsDto, object>($"{baseURL}/copy/user", dto);
            return response.Success;
        }

        public async Task<bool> CopyRolePermissionsAsync(string sourceRoleId, string targetRoleId)
        {
            var dto = new CopyPermissionsDto
            {
                SourceId = sourceRoleId,
                TargetId = targetRoleId,
                EntityType = "Role"
            };

            var response = await httpService.Post<CopyPermissionsDto, object>($"{baseURL}/copy/role", dto);
            return response.Success;
        }

        public async Task<bool> ResetUserPermissionsAsync(string userId)
        {
            var response = await httpService.Delete($"{baseURL}/reset/user/{userId}");
            return response.Success;
        }

        public async Task<bool> ResetRolePermissionsAsync(string roleId)
        {
            var response = await httpService.Delete($"{baseURL}/reset/role/{roleId}");
            return response.Success;
        }

        #endregion

        #region Reports and Audit

        public async Task<List<NavigationPermissionDto>> GetAllNavigationItemsWithPermissionCountAsync()
        {
            var response = await httpService.Get<List<NavigationPermissionDto>>($"{baseURL}/report/navigation-items");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<UserPermissionsDto>> GetAllUsersWithPermissionsAsync()
        {
            var response = await httpService.Get<List<UserPermissionsDto>>($"{baseURL}/report/users-permissions");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<PermissionMatrixDto> GetPermissionMatrixAsync()
        {
            var response = await httpService.Get<PermissionMatrixDto>($"{baseURL}/matrix");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<PermissionAuditDto> GetPermissionAuditAsync(DateTime startDate, DateTime endDate)
        {
            var url = $"{baseURL}/audit?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";
            var response = await httpService.Get<PermissionAuditDto>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Obtiene el ID del usuario actual autenticado
        /// </summary>
        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        /// <summary>
        /// Verifica si el usuario actual tiene un permiso específico
        /// </summary>
        public async Task<bool> CurrentUserHasPermissionAsync(int navigationItemId, string permissionType = "View")
        {
            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId))
                return false;

            try
            {
                var dto = new CheckPermissionDto
                {
                    UserId = userId,
                    NavigationItemId = navigationItemId,
                    PermissionType = permissionType
                };

                return await CheckPermissionAsync(dto);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene el menú de navegación del usuario actual
        /// </summary>
        public async Task<NavigationMenuDto?> GetCurrentUserNavigationMenuAsync()
        {
            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId))
                return null;

            try
            {
                return await GetUserNavigationMenuAsync(userId);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }

    // DTO adicional para copia de permisos
    public class CopyPermissionsDto
    {
        public string SourceId { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public string EntityType { get; set; } = "User"; // User or Role
    }
}