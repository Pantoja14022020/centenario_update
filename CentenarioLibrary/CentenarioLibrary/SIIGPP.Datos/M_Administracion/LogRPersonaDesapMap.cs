using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRPersonaDesapMap : IEntityTypeConfiguration<LogRPersonaDesap>
    {
        public void Configure(EntityTypeBuilder<LogRPersonaDesap> builder)
        {
            builder.ToTable("LOG_PERSONADESAPARECIDA")
                .HasKey(a => a.IdAdminPersonaDesaparecida);

            builder.Property(a => a.IdAdminPersonaDesaparecida)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminPersonaDesaparecida)
                .HasDefaultValueSql("newid()");

            builder.Property(a => a.PersonaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}