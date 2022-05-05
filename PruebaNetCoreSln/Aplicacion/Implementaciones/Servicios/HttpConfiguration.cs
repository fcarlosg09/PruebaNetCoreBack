#region Usings
using Dominio.Interfaces.Servicios;
using Microsoft.AspNetCore.Http;
#endregion

namespace Aplicacion.Implementaciones.Servicios
{
    public class HttpConfiguration : IHttpConfiguration
    {
        #region Dependencias
        private readonly IHttpClientFactory _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public HttpConfiguration(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        #endregion


        public HttpClient HttpClientFactory(string NombreConexion)
        {
            HttpClient? httpClient = _httpClient.CreateClient(NombreConexion);
            return httpClient;
        }
    }
}