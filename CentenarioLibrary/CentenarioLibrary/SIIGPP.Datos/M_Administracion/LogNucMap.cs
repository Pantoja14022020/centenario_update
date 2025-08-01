using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{    
    public class LogNucMap : IEntityTypeConfiguration<LogNuc>
    {
        public void Configure(EntityTypeBuilder<LogNuc> builder)
        {
            builder.ToTable("LOG_NUC")
                .HasKey(a => a.IdAdminNuc);

            builder.Property(a => a.IdAdminNuc)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdminId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.idNuc)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.DistritoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.AgenciaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
        }
    }
}