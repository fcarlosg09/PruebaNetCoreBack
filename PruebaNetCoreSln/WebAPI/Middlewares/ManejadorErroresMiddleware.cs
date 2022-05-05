#region Usings
using Aplicacion.ManejadorErrores;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Net;
#endregion

namespace WebAPI.Middlewares
{
    public class ManejadorErroresMiddleware
    {
        #region Dependencias
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErroresMiddleware> _logger;
        #endregion

        #region Constructor
        public ManejadorErroresMiddleware(RequestDelegate next, ILogger<ManejadorErroresMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Métodos
        #region Invoke
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ManejadorExcepcionAsync(context, e, _logger);
            }
        }
        #endregion

        #region ManejadorExcepcionAsync
        private static async Task ManejadorExcepcionAsync(HttpContext context, Exception e, ILogger<ManejadorErroresMiddleware> logger)
        {
            object errores = null!;
            switch (e)
            {
                case ManejadorExcepcion me:
                    logger.LogError(e, "Manejador Error");
                    errores = me._errores;
                    context.Response.StatusCode = (int)me._codigo;
                    context.Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = me._errores.ToString();
                    break;

                case Exception ex:
                    logger.LogError(e, "Error de Servidor");
                    errores = string.IsNullOrWhiteSpace(ex.Message) ? "Error" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = "Error de servidor";
                    break;
            }

            context.Response.ContentType = "application/json; charset=utf-8";

            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { mensaje = errores });
                await context.Response.WriteAsync(resultados);
            }
        }
        #endregion
        #endregion
    }
}