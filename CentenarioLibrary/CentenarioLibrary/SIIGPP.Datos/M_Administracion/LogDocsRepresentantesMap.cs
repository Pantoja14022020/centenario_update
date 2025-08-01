using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogDocsRepresentantesMap : IEntityTypeConfiguration<LogDocsRepresentantes>
    {
        public void Configure(EntityTypeBuilder<LogDocsRepresentantes> builder)
        {
            builder.ToTable("LOG_DOCSREPRESENTANTES")
                .HasKey(a => a.IdAdminDocsRepresentantes);

            builder.Property(a => a.IdAdminDocsRepresentantes)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RepresentanteId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder
                .Property(a => a.IdAdminDocsRepresentantes)
                .HasDefaultValueSql("newId()");
        }
    }
}