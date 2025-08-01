using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.ArchivosVehiculos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.ArchivosVehiculos
{
    public class ArchivosVehiculoMap : IEntityTypeConfiguration<ArchivoVehiculo>
    {
        public void Configure(EntityTypeBuilder<ArchivoVehiculo> builder)
        {
            builder.ToTable("CAT_ARCHIVOS_VEHICULOS")
                    .HasKey(a => a.IdArchivoVehiculos);

            builder.Property(a => a.IdArchivoVehiculos)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.VehiculoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdArchivoVehiculos)
           .HasDefaultValueSql("newId()");
        }
    }
}
