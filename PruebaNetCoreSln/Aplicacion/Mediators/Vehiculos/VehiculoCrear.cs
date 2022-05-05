#region Usings
using Dominio.DTOs;
using Dominio.Interfaces.Repositorios;
using Dominio.Modelos;
using FluentValidation;
using MediatR;
#endregion

namespace Aplicacion.Mediators.Vehiculos
{
    public class VehiculoCrear
    {
        #region Request
        public class VehiculoCrearCargar : CrearVehiculoModel, IRequest<RespuestaGenericaDTO> { }
        #endregion

        #region Validaciones Request
        public class EjecutaValidacion : AbstractValidator<VehiculoCrearCargar>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Placa!)
                   .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("La placa es requerida")
                       .NotEmpty().WithMessage("La placa es requerida")
                       .Length(8).WithMessage("La placa debe ser de 8 caracteres");

                RuleFor(x => x.Titular!)
                   .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("El titular es requerido")
                       .NotEmpty().WithMessage("El titular es requerido");

                RuleFor(x => x.Marca!)
                   .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("La marca es requerida")
                       .NotEmpty().WithMessage("La marca es requerida")
                       .GreaterThan(0).WithMessage("La marca es requerida");

                RuleFor(x => x.Puertas!)
                   .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Las puertas son requeridas")
                       .NotEmpty().WithMessage("Las puertas son requeridas")
                       .GreaterThan(0).WithMessage("Las puertas son requeridas");

                RuleFor(x => x.Modelo!)
                   .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("El modelo es requerido")
                       .NotEmpty().WithMessage("El modelo es requerido");
            }
        }
        #endregion

        public class Manejador : IRequestHandler<VehiculoCrearCargar, RespuestaGenericaDTO>
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
            public async Task<RespuestaGenericaDTO> Handle(VehiculoCrearCargar request, CancellationToken cancellationToken)
            {
                return await _vehiculosRepository.CrearVehiculoAsync(request).ConfigureAwait(false);
            }
            #endregion
        }
    }
}