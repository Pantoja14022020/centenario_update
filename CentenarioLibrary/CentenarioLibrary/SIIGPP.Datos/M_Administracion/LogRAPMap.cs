using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRAPMap : IEntityTypeConfiguration<LogRAP>
    {
        public void Configure(EntityTypeBuilder<LogRAP> builder)
        {
            builder.ToTable("LOG_RAP")
                .HasKey(a => a.IdAdminRAP);

            builder.Property(a => a.ClasificacionPersona)
                .HasColumnType("nvarchar(200)");

            builder.Property(a => a.IdRAP)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminRAP)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RAtencionId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.PersonaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder
                .Property(a => a.IdRAP)
                .HasDefaultValueSql("newId()");
        }
    }
}