#region Usings
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

namespace Infraestructura.EntityFrameworkCore.Configs
{
    public class ListadoVehiculosConfig : IEntityTypeConfiguration<ListadoVehiculos>
    {
        #region Métodos
        public void Configure(EntityTypeBuilder<ListadoVehiculos> builder)
        {
            builder.ToTable("ListadoVehiculos");
            builder.HasKey(c => c.Placa);

            builder
                .HasOne(s => s.Marcas)
                .WithMany(g => g.ListadoVehiculos_Lista)
                .HasForeignKey(s => s.Marca);
        }
        #endregion
    }
}