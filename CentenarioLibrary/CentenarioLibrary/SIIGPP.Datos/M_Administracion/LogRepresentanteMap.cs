using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRepresentanteMap : IEntityTypeConfiguration<LogRepresentante>
    {
        public void Configure(EntityTypeBuilder<LogRepresentante> builder)
        {
            builder.ToTable("LOG_REPRESENTANTES")
                .HasKey(a => a.IdAdminRepresentante);

            builder.Property(a => a.IdAdminRepresentante)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
            
            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.PersonaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder
                .Property(a => a.IdAdminRepresentante)
                .HasDefaultValueSql("newId()");
        }
    }
}