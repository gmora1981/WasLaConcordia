using LaConcordia.Model;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components.Authorization;
using LaConcordia.Helpers;
using LaConcordia.Repository;
using System.Linq;

namespace LaConcordia.Auth
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly IAccountsRepository accountsRepository;
        private readonly string TOKENKEY = "TOKENKEY";
        private readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private readonly string USERROLEKEY = "USERROLEKEY"; // Nueva clave para almacenar el rol
        private readonly string USERINFKEY = "USERINFKEY"; // Nueva clave para información del usuario

        private AuthenticationState Anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JWTAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient,
            IAccountsRepository usersRepository)
        {
            this.js = js;
            this.httpClient = httpClient;
            this.accountsRepository = usersRepository;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymous;
            }

            var expirationTimeString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime expirationTime;

            if (DateTime.TryParse(expirationTimeString, out expirationTime))
            {
                if (IsTokenExpired(expirationTime))
                {
                    await CleanUp();
                    return Anonymous;
                }

                if (ShouldRenewToken(expirationTime))
                {
                    token = await RenewToken(token);
                }
            }

            return BuildAuthenticationState(token);
        }

        public async Task TryRenewToken()
        {
            var expirationTimeString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime expirationTime;

            if (DateTime.TryParse(expirationTimeString, out expirationTime))
            {
                if (IsTokenExpired(expirationTime))
                {
                    await Logout();
                }

                if (ShouldRenewToken(expirationTime))
                {
                    var token = await js.GetFromLocalStorage(TOKENKEY);
                    var newToken = await RenewToken(token);
                    var authState = BuildAuthenticationState(newToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private async Task<string> RenewToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var newToken = await accountsRepository.RenewToken();
            await js.SetInLocalStorage(TOKENKEY, newToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, newToken.Expiration.ToString());

            // Reextraer y guardar los roles del nuevo token
            await SaveUserRoleFromToken(newToken.Token);
            return newToken.Token;
        }

        private bool ShouldRenewToken(DateTime expirationTime)
        {
            return expirationTime.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private bool IsTokenExpired(DateTime expirationTime)
        {
            return expirationTime <= DateTime.UtcNow;
        }

        public AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        // Método para extraer y guardar el rol del token
        private async Task SaveUserRoleFromToken(string token)
        {
            try
            {
                var claims = ParseClaimsFromJwt(token);
                var roleClaims = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

                if (roleClaims.Any())
                {
                    if (roleClaims.Count == 1)
                    {
                        // Un solo rol
                        await js.SetInLocalStorage(USERROLEKEY, roleClaims.First().Value);
                    }
                    else
                    {
                        // Múltiples roles - guardamos como JSON array
                        var rolesArray = roleClaims.Select(r => r.Value).ToArray();
                        var rolesJson = JsonSerializer.Serialize(rolesArray);
                        await js.SetInLocalStorage(USERROLEKEY, rolesJson);
                    }
                }

                // Opcionalmente, guardar otra información del usuario
                var userInfo = new
                {
                    Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    UserId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                };

                if (!string.IsNullOrEmpty(userInfo.Email) || !string.IsNullOrEmpty(userInfo.Name))
                {
                    var userInfoJson = JsonSerializer.Serialize(userInfo);
                    await js.SetInLocalStorage(USERINFKEY, userInfoJson);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar rol en localStorage: {ex.Message}");
            }
        }
        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(TOKENKEY, userToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, userToken.Expiration.ToString());

            // Extraer y guardar el rol del token
            await SaveUserRoleFromToken(userToken.Token);

            var authState = BuildAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await CleanUp();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        private async Task CleanUp()
        {
            await js.RemoveItem(TOKENKEY);
            await js.RemoveItem(EXPIRATIONTOKENKEY);
            await js.RemoveItem(USERROLEKEY);  // Limpiar el rol
            await js.RemoveItem(USERINFKEY);   // Limpiar info del usuario
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Métodos públicos para obtener roles e información del usuario desde localStorage
        public async Task<string> GetUserRoleAsync()
        {
            return await js.GetFromLocalStorage(USERROLEKEY);
        }

        public async Task<string[]> GetUserRolesAsync()
        {
            var rolesData = await js.GetFromLocalStorage(USERROLEKEY);

            if (string.IsNullOrEmpty(rolesData))
                return new string[0];

            try
            {
                // Intentar deserializar como array
                if (rolesData.StartsWith("["))
                {
                    return JsonSerializer.Deserialize<string[]>(rolesData);
                }
                else
                {
                    // Es un solo rol
                    return new string[] { rolesData };
                }
            }
            catch
            {
                // Si falla la deserialización, tratarlo como string simple
                return new string[] { rolesData };
            }
        }

        public async Task<UserInfo> GetUserInfoAsync()
        {
            var userInfoData = await js.GetFromLocalStorage(USERINFKEY);

            if (string.IsNullOrEmpty(userInfoData))
                return null;

            try
            {
                return JsonSerializer.Deserialize<UserInfo>(userInfoData);
            }
            catch
            {
                return null;
            }
        }
    }
}
