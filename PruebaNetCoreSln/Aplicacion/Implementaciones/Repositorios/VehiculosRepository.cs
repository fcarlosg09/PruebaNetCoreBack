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
    public class VehiculosRepository : IVehiculosRepository
    {
        #region Dependencias
        private readonly VehiculosDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public VehiculosRepository(VehiculosDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Métodos
        #region CrearMarcaAsync
        public async Task<RespuestaGenericaDTO> CrearVehiculoAsync(CrearVehiculoModel parametros)
        {
            #region Validar si existe marca
            var marcaValidarSiExiste = await _context.Marcas
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == parametros.Marca)
                                        .ConfigureAwait(false);

            if (marcaValidarSiExiste == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ExcepcionesCustom.Excepcion_MarcaNoExiste);
            }
            #endregion

            var placaValidarSiExiste = await _context.ListadoVehiculos
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.Placa!.Trim().ToLower() == parametros.Placa!.Trim().ToLower())
                                       .ConfigureAwait(false);

            if (placaValidarSiExiste != null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ExcepcionesCustom.Excepcion_VehiculoExiste);
            }

            ListadoVehiculos vehiculos = new()
            {
                Marca = parametros.Marca,
                Modelo = parametros.Modelo!.Trim(),
                Placa = parametros.Placa!.Trim(),
                Puertas = parametros.Puertas,
                Titular = parametros.Titular!.Trim()
            };


            _context.ListadoVehiculos!.Add(vehiculos);

            int valor = _context.SaveChanges();
            if (valor > 0)
            {
                RespuestaGenericaDTO respuestaGenerica = new()
                {
                    Mensaje = "Se creó el vehículo correctamente"
                };

                return respuestaGenerica;
            }

            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ExcepcionesCustom.Excepcion_VehiculoNoSeCreo);
        }
        #endregion

        #region ListarAsync
        public async Task<IReadOnlyList<VehiculosCompactoDTO>> ListarAsync()
        {
            var marcas = await _context.Marcas
                                        .AsNoTracking()
                                        .ToListAsync()
                                        .ConfigureAwait(false);

            var vehiculos = await _context.ListadoVehiculos
                                                .AsNoTracking()
                                                .ToListAsync()
                                                .ConfigureAwait(false);

            var resultados = from veh in vehiculos
                             join marca in marcas
                             on veh.Marca equals marca.Id
                             select new VehiculosCompactoDTO (){ MarcaNombre = marca.Nombre, Placa = veh.Placa, Titular = veh.Titular, Modelo = veh.Modelo, Puertas = veh.Puertas, Marca = veh.Marca };

            return resultados.ToList();
        }
        #endregion
        #endregion
    }
}