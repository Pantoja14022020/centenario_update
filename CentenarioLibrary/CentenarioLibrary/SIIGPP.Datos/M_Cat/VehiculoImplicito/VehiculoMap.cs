using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.VehiculoImplicito;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_Cat.VehiculoImplicito
{
    public class VehiculoMap : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("CAT_VEHICULO")
                .HasKey(a => a.IdVehiculo);

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

            builder.Property(a => a.IdVehiculo)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdVehiculo)
           .HasDefaultValueSql("newId()");
        }

    }
}
