#region Usings
using Dominio.DTOs;
using Dominio.Interfaces.Repositorios;
using Dominio.Modelos;
using FluentValidation;
using MediatR;
#endregion

namespace Aplicacion.Mediators.Marcas
{
    public class MarcaCrear
    {
        #region Request
        public class MarcaCrearCargar : CrearMarcaModel, IRequest<RespuestaGenericaDTO> { }
        #endregion

        #region Validaciones Request
        public class EjecutaValidacion : AbstractValidator<MarcaCrearCargar>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre!)
                   .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("La marca es requerida")
                       .NotEmpty().WithMessage("La marca es requerida");
            }
        }
        #endregion

        public class Manejador : IRequestHandler<MarcaCrearCargar, RespuestaGenericaDTO>
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
            public async Task<RespuestaGenericaDTO> Handle(MarcaCrearCargar request, CancellationToken cancellationToken)
            {
                return await _marcasRepository.CrearMarcaAsync(request).ConfigureAwait(false);
            }
            #endregion
        }
    }
}