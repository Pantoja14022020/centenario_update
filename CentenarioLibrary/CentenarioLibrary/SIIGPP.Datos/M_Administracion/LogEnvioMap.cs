using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogEnvioMap : IEntityTypeConfiguration<LogEnvio>
    {
        public void Configure(EntityTypeBuilder<LogEnvio> builder)
        {
            builder.ToTable("LOG_ENVIO")
                .HasKey(a => a.IdAdminEnvio);

            builder.Property(a => a.FechaRegistro)
                .HasColumnType("DateTime");

            builder.Property(a => a.FechaCierre)
                .HasColumnType("DateTime");

            builder.Property(a => a.IdAdminEnvio)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.ExpedienteId)
                .HasColumnType("UNIQUEIDENTIFIER ")
                .IsRequired();
            
            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
            
            builder.Property(a => a.IdAdminEnvio)
                .HasDefaultValueSql("newId()");
        }    
    }
}