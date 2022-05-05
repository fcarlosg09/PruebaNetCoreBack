using Aplicacion.Mediators.Usuarios;
using Aplicacion.Mediators.Vehiculos;
using Dominio.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        #region Dependencias
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public VehiculosController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region Listar
        [HttpGet]
        public async Task<IReadOnlyList<VehiculosCompactoDTO>> Listar()
        {
            return await _mediator.Send(new VehiculoListar.VehiculoListarCargar());
        }
        #endregion

        #region Agregar
        [HttpPost]
        public async Task<RespuestaGenericaDTO> Agregar([FromBody] VehiculoCrear.VehiculoCrearCargar parametros)
        {
            return await _mediator.Send(parametros);
        }
        #endregion

        #region ListarUsuarios
        [HttpGet("Usuarios")]
        public async Task<IReadOnlyList<UsuariosCompactoDTO>> ListarUsuarios()
        {
            return await _mediator.Send(new UsuarioListar.UsuarioListarCargar());
        }
        #endregion
    }
}
