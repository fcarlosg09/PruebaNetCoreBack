#region Usings
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

namespace Infraestructura.EntityFrameworkCore.Configs
{
    public class MarcasConfig : IEntityTypeConfiguration<Marcas>
    {
        #region Métodos
        public void Configure(EntityTypeBuilder<Marcas> builder)
        {
            builder.ToTable("Marcas");
            builder.HasKey(c => c.Id);

            builder
                .HasMany(s => s.ListadoVehiculos_Lista)
                .WithOne(g => g.Marcas)
                .HasForeignKey(c => c.Marca);
        }
        #endregion
    }
}
