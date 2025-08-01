using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Datos.M_Configuracion.Cat_Vehiculos
{
    public class TipovMap : IEntityTypeConfiguration<Tipov>
    {
        public void Configure(EntityTypeBuilder<Tipov> builder)
        {
            builder.ToTable("CV_TIPOV")
                .HasKey(a => a.IdTipov);
            builder
           .Property(a => a.IdTipov)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipov)
           .HasDefaultValueSql("newId()");
        }

    }
}
