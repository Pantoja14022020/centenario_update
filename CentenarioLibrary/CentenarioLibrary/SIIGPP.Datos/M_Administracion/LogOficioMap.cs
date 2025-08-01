using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogOficioMap : IEntityTypeConfiguration<LogOficio>
    {
        public void Configure(EntityTypeBuilder<LogOficio> builder)
        {
            builder.ToTable("LOG_OFICIO")
                .HasKey(a => a.IdAdminOficios);

            builder.Property(a => a.IdAdminOficios)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder
                .Property(a => a.IdAdminOficios)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}