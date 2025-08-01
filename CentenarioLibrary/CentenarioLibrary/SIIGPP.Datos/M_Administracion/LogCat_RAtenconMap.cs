using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{    
    public class LogCat_RAtenconMap : IEntityTypeConfiguration<LogCat_RAtencon>
    {
        public void Configure(EntityTypeBuilder<LogCat_RAtencon> builder)
        {
            builder.ToTable("LOG_RATENCON")
                .HasKey(a => a.IdRAtencion);

            builder.Property(a => a.IdRAtencion)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.racId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
        }
    }
}