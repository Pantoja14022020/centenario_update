using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogDesgloseMap : IEntityTypeConfiguration<LogDesglose>
    {
        public void Configure(EntityTypeBuilder<LogDesglose> builder)
        {
            builder.ToTable("LOG_DESGLOSES")
                .HasKey(a => a.IdAdminDesglose);

            builder.Property(a => a.FechaDesglose)
                .HasColumnType("DateTime");

            builder.Property(a => a.IdAdminDesglose)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.DesgloseId)
                .HasColumnType("UNIQUEIDENTIFIER ")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminDesglose)
                .HasDefaultValueSql("newId()");
        }
    }
}