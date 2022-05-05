#region Usings
using AutoMapper;
using Dominio.DTOs;
using Dominio.Entidades;
#endregion

namespace Aplicacion.AutoMapper
{
    public class MappingProfiles : Profile
    {
        #region Constructor
        public MappingProfiles()
        {
            CreateMap<Marcas, MarcasDTO>();
            CreateMap<ListadoVehiculos, VehiculosDTO>();
        }
        #endregion
    }
}