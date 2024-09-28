using ConsumoAPI.Components;
using ConsumoAPI.DTO;
using System.Net.Http.Headers;
namespace ConsumoAPI.Services
{
    public class InformeService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public InformeService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<InformeResponse>> GetInformes()
        {
            try
            {
                var token = await _authService.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. iniciar secion");

                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<InformeResponse>>("api/informes");
                return response;
            }

            catch (HttpRequestException ex) {

                throw new Exception("Error al obtener informes. Resvisar conexion a internet.");
            }
            catch (Exception ex) {
                throw new Exception("Ha ocurrido un error inesperado al obtener informes.");
            }
        }
    }
}
