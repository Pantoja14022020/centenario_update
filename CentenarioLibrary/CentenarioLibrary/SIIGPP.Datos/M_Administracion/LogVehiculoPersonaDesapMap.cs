using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogVehiculoPersonaDesapMap : IEntityTypeConfiguration<LogVehiculoPersonaDesap>
    {
        public void Configure(EntityTypeBuilder<LogVehiculoPersonaDesap> builder)
        {
            builder.ToTable("LOG_VEHICULO_DESAPARICIONPERSONA")
                .HasKey(a => a.IdAdminVehDesaparicionPersona);

            builder.Property(a => a.IdAdminVehDesaparicionPersona)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminVehDesaparicionPersona)
                .HasDefaultValueSql("newid()");

            builder.Property(a => a.PersonaDesaparecidaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
               .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
               .IsRequired();
        }
    }
}