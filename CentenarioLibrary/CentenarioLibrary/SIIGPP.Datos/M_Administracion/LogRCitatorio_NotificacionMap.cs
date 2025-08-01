using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRCitatorioNotificacionMap : IEntityTypeConfiguration<LogRCitatorioNotificacion>
    {
        public void Configure(EntityTypeBuilder<LogRCitatorioNotificacion> builder)
        {
            builder.ToTable("LOG_RCITATORIO_NOTIFICACION")
                .HasKey(a => a.IdAdminCitatorio_Notificacion);

            builder.Property(a => a.Puesto)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Usuario)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Subproc)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Agencia)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Distrito)
                .HasColumnType("nvarchar(500)");
            
            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminCitatorio_Notificacion)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder
                .Property(a => a.IdAdminCitatorio_Notificacion)
                .HasDefaultValueSql("newId()");
        }
    }
}