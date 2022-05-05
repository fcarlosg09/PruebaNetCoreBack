#region Usings
using Dominio.DTOs;
using Dominio.Modelos;
#endregion

namespace Dominio.Interfaces.Repositorios
{
    public interface IVehiculosRepository
    {
        #region Métodos
        Task<IReadOnlyList<VehiculosCompactoDTO>> ListarAsync();
        Task<RespuestaGenericaDTO> CrearVehiculoAsync(CrearVehiculoModel parametros);
        #endregion
    }
}