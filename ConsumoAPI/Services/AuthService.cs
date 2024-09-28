using ConsumoAPI.DTO;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;

namespace ConsumoAPI.Services
{
    public class AuthService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly HttpClient _httpClient;
        private string? _token;

        public AuthService(ProtectedLocalStorage localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }
        //Enviar datos a enpoint Login 
        public async Task<string> Login(UserSession userSession)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuarios/Login", userSession);
            if (response.IsSuccessStatusCode)
            {
                // Deserializar la respuesta JSON a un objeto LoginResponse
                var result = await response.Content.ReadFromJsonAsync<string>();

                // Verificar si el objeto no es nulo y devolver el token
                return result;

            }
            return null;
        }

        //Guardar token en el navegador 
        public async Task SetToken(string token)
        {
            _token = token;
            await _localStorage.SetAsync("token", token);
        }

        //Obtener token del navegador 
        public async Task<string> GetToken()
        {

            var localStorageResult = await _localStorage.GetAsync<string>("token");

            if (string.IsNullOrEmpty(_token))
            {

                if (!localStorageResult.Success || string.IsNullOrEmpty(localStorageResult.Value))
                {
                    _token = null;
                    return null;
                }
                _token = localStorageResult.Value;

            }
            return _token;
        }

        //Verificar si el usuario esta autenticado 
        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();

            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        //Verificar si el token a expirado

        public bool IsTokenExpired(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }
        //cerrar sesion
        public async Task Logout()
        {
            _token = null;
            await _localStorage.DeleteAsync("token");
        }
    }
}