using SIIGPP.Entidades.M_Administracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_Cat.VehiculoImplicito
{
    public class LogVehiculoMap : IEntityTypeConfiguration<LogVehiculo>
    {
        public void Configure(EntityTypeBuilder<LogVehiculo> builder)
        {
            builder.ToTable("LOG_VEHICULO")
                .HasKey(a => a.IdAdminVehiculo);

            builder.Property(a => a.Puesto)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Usuario)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Subproc)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Agencia)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Distrito)
                .HasColumnType("nvarchar(500)");

            builder.Property(a => a.IdAdminVehiculo)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.IdAdminVehiculo)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}