using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRDiligenciasForaneasMap : IEntityTypeConfiguration<LogRDiligenciasForaneas>
    {
        public void Configure(EntityTypeBuilder<LogRDiligenciasForaneas> builder)
        {
            builder.ToTable("LOG_RDILIGENCIASFORANEAS")
                .HasKey(a => a.IdAdminRdiligenciasForaneas);

            builder.Property(a => a.IdAdminRdiligenciasForaneas)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminRdiligenciasForaneas)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.ASPId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}