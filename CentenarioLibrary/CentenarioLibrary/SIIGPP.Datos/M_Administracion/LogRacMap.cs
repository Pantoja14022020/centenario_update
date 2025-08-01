using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{    
    public class LogRacMap : IEntityTypeConfiguration<LogRac>
    {
        public void Configure(EntityTypeBuilder<LogRac> builder)
        {
            builder.ToTable("LOG_RAC")
                .HasKey(a => a.IdAdminRac);

            builder.Property(a => a.IdAdminRac)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.idRac)
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