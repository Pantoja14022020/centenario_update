using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogBitacoraMap : IEntityTypeConfiguration<LogBitacora>
    {
        public void Configure(EntityTypeBuilder<LogBitacora> builder)
        {
            builder.ToTable("LOG_BITACORA")
                .HasKey(a => a.IdAdminBitacora);

            builder.Property(a => a.IdAdminBitacora)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL");

            builder.Property(a => a.IdAdminBitacora)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminBitacora)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdPersona)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.rHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.IdBitacora)
                .HasDefaultValueSql("newId()");
        }
    }
}