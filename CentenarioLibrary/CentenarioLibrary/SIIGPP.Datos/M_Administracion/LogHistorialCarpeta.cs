using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogHistorialcarpetasMap : IEntityTypeConfiguration<LogHistorialCarpeta>
    {
        public void Configure(EntityTypeBuilder<LogHistorialCarpeta> builder)
        {
            builder.ToTable("LOG_HISTORIAL_CARPETA")
                    .HasKey(a => a.IdAdminHistorialcarpetas);

            builder.Property(a => a.IdAdminHistorialcarpetas)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder
                .Property(a => a.IdAdminHistorialcarpetas)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}