using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogExpedienteMap : IEntityTypeConfiguration<LogExpediente>
    {
        public void Configure(EntityTypeBuilder<LogExpediente> builder)
        {
            builder.ToTable("LOG_EXPEDIENTE")
                .HasKey(a => a.IdAdminExpediente);

            builder.Property(a => a.FechaRegistroExpediente)
                .HasColumnType("DateTime");

            builder.Property(a => a.FechaDerivacion)
               .HasColumnType("DateTime");

            builder.Property(a => a.IdAdminExpediente)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER ")
                .IsRequired();

            builder.Property(a => a.IdAdminExpediente)
                .HasDefaultValueSql("newId()");
            
            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}