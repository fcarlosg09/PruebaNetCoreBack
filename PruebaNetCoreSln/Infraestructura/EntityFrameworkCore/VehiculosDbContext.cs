#region Usings
using Dominio.Entidades;
using Infraestructura.EntityFrameworkCore.Configs;
using Microsoft.EntityFrameworkCore;
#endregion

namespace Infraestructura.EntityFrameworkCore
{
    public class VehiculosDbContext : DbContext
    {
        #region Constructor
        public VehiculosDbContext(DbContextOptions options) : base(options) { }
        #endregion

        #region Métodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Configuraciones
            modelBuilder.ApplyConfiguration(new MarcasConfig());
            modelBuilder.ApplyConfiguration(new ListadoVehiculosConfig());
            #endregion
        }
        #endregion

        #region DbSet
        public DbSet<Marcas> Marcas { get; set; }
        public DbSet<ListadoVehiculos> ListadoVehiculos { get; set; }
        #endregion
    }
}