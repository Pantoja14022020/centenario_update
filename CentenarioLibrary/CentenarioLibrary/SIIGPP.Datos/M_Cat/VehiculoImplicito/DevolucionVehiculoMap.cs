using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.VehiculoImplicito;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_Cat.VehiculoImplicito
{
    public class DevolucionvehiculoMap : IEntityTypeConfiguration<DevolucionVehiculo>
    {
        public void Configure(EntityTypeBuilder<DevolucionVehiculo> builder)
        {
            builder.ToTable("CAT_DEVOLUCIONVEHICULO")
                .HasKey(a => a.IdDevolucionVehiculo);

            builder.Property(a => a.IdDevolucionVehiculo)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.VehiculoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDevolucionVehiculo)
           .HasDefaultValueSql("newId()");
        }

    }
}
