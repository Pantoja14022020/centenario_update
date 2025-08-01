using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Cat.MedidasProteccion
{
    public class LogMedidasProteccionMap : IEntityTypeConfiguration<LogMedidasproteccion>
    {
        public void Configure(EntityTypeBuilder<LogMedidasproteccion> builder)
        {
            builder.ToTable("LOG_MEDIDASPROTECCION")
                .HasKey(a => a.IdAdminMProteccion);

            builder.Property(a => a.Puesto)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Usuario)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Subproc)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.UAgencia)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Distrito)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.IdAdminMProteccion)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.IdAdminMProteccion)
                .HasDefaultValueSql("newId()");
        }
    }
}