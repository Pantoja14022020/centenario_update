using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Datos.M_Configuracion.Cat_Vehiculos
{
    public class ModeloMap : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.ToTable("CV_MODELO")
                .HasKey(a => a.IdModelo);
            builder
           .Property(a => a.IdModelo)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.MarcaId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();
            builder
           .Property(a => a.IdModelo)
           .HasDefaultValueSql("newId()");
        }

    }
}
