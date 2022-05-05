#region Usings
using Dominio.DTOs;
using Dominio.Interfaces.Repositorios;
using MediatR;
#endregion

namespace Aplicacion.Mediators.Marcas
{
    public class MarcaListar
    {
        #region Request
        public class MarcaListarCargar : IRequest<IReadOnlyList<MarcasDTO>> { }
        #endregion

        public class Manejador : IRequestHandler<MarcaListarCargar, IReadOnlyList<MarcasDTO>>
        {
            #region Dependencias
            private readonly IMarcasRepository _marcasRepository;
            #endregion

            #region Constructor
            public Manejador(IMarcasRepository marcasRepository)
            {
                _marcasRepository = marcasRepository ?? throw new ArgumentNullException(nameof(marcasRepository));
            }
            #endregion

            #region Métodos
            public async Task<IReadOnlyList<MarcasDTO>> Handle(MarcaListarCargar request, CancellationToken cancellationToken)
            {
                return await _marcasRepository.ListarAsync().ConfigureAwait(false);
            }
            #endregion
        }
    }
}