using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogCat_RHechoMap : IEntityTypeConfiguration<LogCat_RHecho>
    {
        public void Configure(EntityTypeBuilder<LogCat_RHecho> builder)
        {
            builder.ToTable("LOG_RHECHO")
                .HasKey(a => a.IdAdminRHecho);

            builder.Property(a => a.IdAdminRHecho)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdRHecho)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RAtencionId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.ModuloServicioId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.Agenciaid)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.NucId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
        }
    }
}