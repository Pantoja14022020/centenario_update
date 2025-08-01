using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogAdmonMap : IEntityTypeConfiguration<LogAdmon>
    {
        public void Configure(EntityTypeBuilder<LogAdmon> builder)
        {
            builder.ToTable("LOG_ADMON")
                .HasKey(a => a.IdLogAdmon);

            builder.Property(a => a.IdLogAdmon)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdLogAdmon)
                .HasDefaultValueSql("newid()");

            builder.Property(a => a.UsuarioId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.RegistroId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.SolicitanteId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.MovimientoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
        }
    }
}