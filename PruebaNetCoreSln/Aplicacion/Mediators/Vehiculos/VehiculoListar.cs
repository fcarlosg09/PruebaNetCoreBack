#region Usings
using Dominio.DTOs;
using Dominio.Interfaces.Repositorios;
using MediatR;
#endregion

namespace Aplicacion.Mediators.Vehiculos
{
    public class VehiculoListar
    {
        #region Request
        public class VehiculoListarCargar : IRequest<IReadOnlyList<VehiculosCompactoDTO>> { }
        #endregion

        public class Manejador : IRequestHandler<VehiculoListarCargar, IReadOnlyList<VehiculosCompactoDTO>>
        {
            #region Dependencias
            private readonly IVehiculosRepository _vehiculosRepository;
            #endregion

            #region Constructor
            public Manejador(IVehiculosRepository vehiculosRepository)
            {
                _vehiculosRepository = vehiculosRepository ?? throw new ArgumentNullException(nameof(vehiculosRepository));
            }
            #endregion

            #region Métodos
            public async Task<IReadOnlyList<VehiculosCompactoDTO>> Handle(VehiculoListarCargar request, CancellationToken cancellationToken)
            {
                return await _vehiculosRepository.ListarAsync().ConfigureAwait(false);
            }
            #endregion
        }
    }
}