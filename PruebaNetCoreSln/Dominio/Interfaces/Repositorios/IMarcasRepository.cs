#region Usings
using Dominio.DTOs;
using Dominio.Modelos;
#endregion

namespace Dominio.Interfaces.Repositorios
{
    public interface IMarcasRepository
    {
        #region Métodos
        Task<IReadOnlyList<MarcasDTO>> ListarAsync();
        Task<RespuestaGenericaDTO> CrearMarcaAsync(CrearMarcaModel parametros);
        #endregion
    }
}