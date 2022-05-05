#region Usings
using Aplicacion.ManejadorErrores;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Entidades;
using Dominio.Excepciones;
using Dominio.Interfaces.Repositorios;
using Dominio.Modelos;
using Infraestructura.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
#endregion

namespace Aplicacion.Implementaciones.Repositorios
{
    public class MarcasRepository : IMarcasRepository
    {
        #region Dependencias
        private readonly VehiculosDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public MarcasRepository(VehiculosDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Métodos
        #region CrearMarcaAsync
        public async Task<RespuestaGenericaDTO> CrearMarcaAsync(CrearMarcaModel parametros)
        {
            var marcaValidarSiExiste = await _context.Marcas
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Nombre!.ToLower().Trim() == parametros.Nombre!.ToLower().Trim())
                                        .ConfigureAwait(false);

            if (marcaValidarSiExiste != null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ExcepcionesCustom.Excepcion_MarcaExiste);
            }

            Marcas nuevaMarca = new()
            {
                Nombre = parametros.Nombre!.Trim()
            };

            _context.Marcas!.Add(nuevaMarca);

            int valor = _context.SaveChanges();
            if (valor > 0)
            {
                RespuestaGenericaDTO respuestaGenerica = new()
                {
                    Mensaje = "Se creó la marca correctamente"
                };

                return respuestaGenerica;
            }

            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ExcepcionesCustom.Excepcion_MarcaNoSeCreo);
        }
        #endregion

        #region ListarAsync
        public async Task<IReadOnlyList<MarcasDTO>> ListarAsync()
        {
            var marcas = await _context.Marcas
                                        .AsNoTracking()
                                        .ToListAsync()
                                        .ConfigureAwait(false);

            var marcasDTO = _mapper.Map<List<Marcas>, List<MarcasDTO>>(marcas);
            return marcasDTO;
        }
        #endregion
        #endregion
    }
}