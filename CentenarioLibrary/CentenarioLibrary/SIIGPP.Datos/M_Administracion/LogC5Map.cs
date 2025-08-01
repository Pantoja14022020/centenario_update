using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogC5Map : IEntityTypeConfiguration<LogC5Formatos>
    {
        public void Configure(EntityTypeBuilder<LogC5Formatos> builder)
        {
            builder.ToTable("LOG_C5FORMATOS")
                .HasKey(a => a.IdAdminC5);

            builder.Property(a => a.IdAdminC5)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminC5)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}
