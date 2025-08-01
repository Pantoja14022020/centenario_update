using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogAmpDecMap : IEntityTypeConfiguration<LogAmpDec>
    {
        public void Configure(EntityTypeBuilder<LogAmpDec> builder)
        {
            builder.ToTable("LOG_AMPDEC")
               .HasKey(a => a.IdAdminAmpliacion);

            builder.Property(a => a.Fechasys)
               .HasColumnType("DateTime");

            builder.Property(a => a.IdAdminAmpliacion)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.idAmpliacion)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();            

            builder.Property(a => a.HechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.PersonaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.idAmpliacion)
                .HasDefaultValueSql("newId()");
        }
    }
}