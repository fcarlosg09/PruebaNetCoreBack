using Aplicacion.Mediators.Marcas;
using Dominio.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        #region Dependencias
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public MarcasController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region Listar
        [HttpGet]
        public async Task<IReadOnlyList<MarcasDTO>> Listar()
        {
            return await _mediator.Send(new MarcaListar.MarcaListarCargar());
        }
        #endregion

        #region Agregar
        [HttpPost]
        public async Task<RespuestaGenericaDTO> Agregar([FromBody] MarcaCrear.MarcaCrearCargar parametros)
        {
            return await _mediator.Send(parametros);
        }
        #endregion
    }
}