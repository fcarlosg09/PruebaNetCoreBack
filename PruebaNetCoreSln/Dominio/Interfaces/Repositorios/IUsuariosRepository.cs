#region Usings
using Dominio.DTOs;
#endregion

namespace Dominio.Interfaces.Repositorios
{
    public interface IUsuariosRepository
    {
        #region Métodos
        Task<EstructuraGeneralUsuariosDTO> HttpInfoCliente();
        #endregion
    }
}