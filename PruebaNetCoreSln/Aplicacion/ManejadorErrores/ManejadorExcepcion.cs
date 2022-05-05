#region Usings
using System.Net;
#endregion

namespace Aplicacion.ManejadorErrores
{
    public class ManejadorExcepcion : Exception
    {
        #region Propiedades
        public HttpStatusCode _codigo { get; }
        public string _errores { get; set; }
        #endregion

        #region Constructor
        public ManejadorExcepcion(HttpStatusCode codigo, string errores)
        {
            _codigo = codigo;
            _errores = errores;
        }
        #endregion
    }
}