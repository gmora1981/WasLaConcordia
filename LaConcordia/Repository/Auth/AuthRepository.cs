using LaConcordia.Interface;
using LaConcordia.Model;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LaConcordia.Helpers;
using LaConcordia.Auth;

namespace LaConcordia.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpService _httpService;
        private readonly ILoginService _loginService;

        public AuthRepository(HttpClient httpClient, IHttpService httpService, ILoginService loginService)
        {
            _httpClient = httpClient;
            _httpService = httpService;
            _loginService = loginService;
        }

        public async Task<UserToken> Login(UserLogin userLogin)
        {
            try
            {
                var response = await _httpService.Post<UserLogin, UserToken>("api/Accounts/Login", userLogin);

                if (!response.Success)
                {
                    var errorMessage = await response.GetBody();
                    throw new Exception($"Error al iniciar sesión: {errorMessage}");
                }

                // Guardar token en el servicio de login
                await _loginService.Login(response.Response);

                return response.Response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al servidor", ex);
            }
        }

        public async Task<UserToken> Register(UserEditDTO userInfo)
        {
            try
            {
                var response = await _httpService.Post<UserEditDTO, UserToken>("api/Accounts/create", userInfo);

                if (!response.Success)
                {
                    var errorMessage = await response.GetBody();
                    throw new Exception($"Error al registrar usuario: {errorMessage}");
                }

                return response.Response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al servidor", ex);
            }
        }

        public async Task<UserToken> RenewToken()
        {
            try
            {
                var response = await _httpService.Get<UserToken>("api/Accounts/RenewToken");

                if (!response.Success)
                {
                    throw new Exception("Error al renovar el token");
                }

                return response.Response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al renovar token: {ex.Message}", ex);
            }
        }

        public async Task Logout()
        {
            try
            {
                await _loginService.Logout();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cerrar sesión: {ex.Message}", ex);
            }
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordDTO changePassword)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Accounts/ChangePassword/{userId}", changePassword);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al cambiar contraseña: {errorContent}");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al servidor", ex);
            }
        }

        public async Task<bool> ResetPassword(string email)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Accounts/ResetPassword", new { Email = email });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al resetear contraseña: {errorContent}");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error de conexión al servidor", ex);
            }
        }

        public async Task<bool> IsAuthenticated()
        {
            try
            {
                //var token = await _loginService.GetToken();
                //return !string.IsNullOrEmpty(token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserInfo> GetCurrentUser()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<UserInfo>("api/Accounts/CurrentUser");
                return response ?? throw new Exception("No se pudo obtener información del usuario");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener usuario actual", ex);
            }
        }
    }
}