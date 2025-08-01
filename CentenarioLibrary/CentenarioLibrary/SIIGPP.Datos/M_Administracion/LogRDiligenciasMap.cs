using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRDiligenciasMap : IEntityTypeConfiguration<LogRDiligencias>
    {
        public void Configure(EntityTypeBuilder<LogRDiligencias> builder)
        {
            builder.ToTable("LOG_RDILIGENCIAS")
                .HasKey(a => a.IdAdminRDiligencias);

            builder.Property(a => a.IdAdminRDiligencias)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminRDiligencias)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.rHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.ASPId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.DistritoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
        }
    }
}