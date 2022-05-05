#region Usings
using Aplicacion.ManejadorErrores;
using Dominio.DTOs;
using Dominio.Excepciones;
using Dominio.Interfaces.Repositorios;
using Dominio.Interfaces.Servicios;
using Newtonsoft.Json;
using System.Net;
#endregion

namespace Aplicacion.Implementaciones.Repositorios
{
    public class UsuariosRepository : IUsuariosRepository
    {
        #region Dependencias
        private readonly IHttpConfiguration _httpConfiguration;
        #endregion

        #region Constructor
        public UsuariosRepository(IHttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration ?? throw new ArgumentNullException(nameof(httpConfiguration));
        }
        #endregion

        #region Métodos
        #region HttpInfoCliente
        public async Task<EstructuraGeneralUsuariosDTO> HttpInfoCliente()
        {
            HttpClient clientesService = _httpConfiguration.HttpClientFactory("Clientes");

            var responseCliente = await clientesService.GetAsync("").ConfigureAwait(false);
            if(responseCliente.IsSuccessStatusCode)
            {
                EstructuraGeneralUsuariosDTO? resultado = JsonConvert.DeserializeObject<EstructuraGeneralUsuariosDTO>(await responseCliente.Content.ReadAsStringAsync());
                return resultado!;
            }

            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ExcepcionesCustom.Excepcion_ErrorConsultaUsuarios);
        }
        #endregion
        #endregion
    }
}