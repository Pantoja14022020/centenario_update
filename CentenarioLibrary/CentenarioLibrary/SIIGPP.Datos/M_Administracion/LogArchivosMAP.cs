using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogArchivosMAP : IEntityTypeConfiguration<LogArchivos>
    {
        public void Configure(EntityTypeBuilder<LogArchivos> builder)
        {
            builder.ToTable("LOG_ARCHIVOS")
                .HasKey(a => a.IdAdminArchivos);

            builder.Property(a => a.IdAdminArchivos)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminArchivos)
                .HasDefaultValueSql("newId()");
        }
    }
}