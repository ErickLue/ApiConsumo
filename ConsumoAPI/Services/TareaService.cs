using ConsumoAPI.DTO;
using System.Net.Http.Headers;

namespace ConsumoAPI.Services
{
    public class TareaService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public TareaService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<TareaResponse>> GetTareas()
        {
            try
            {
                var token = await _authService.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. iniciar secion");

                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<TareaResponse>>("api/tareas");
                return response;
            }

            catch (HttpRequestException ex)
            {

                throw new Exception("Error al obtener tarea. Resvisar conexion a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener tarea.");
            }
        }

    }
}
