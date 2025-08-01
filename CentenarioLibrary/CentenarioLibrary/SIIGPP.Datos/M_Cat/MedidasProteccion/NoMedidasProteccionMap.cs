using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedidasProteccion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedidasProteccion
{
    public class NoMedidasProteccionMap : IEntityTypeConfiguration<NoMedidasProteccion>
    {
        public void Configure(EntityTypeBuilder<NoMedidasProteccion> builder)
        {
            builder.ToTable("CAT_NO_MEDIDASPROTECCION")
                .HasKey(a => a.IdNoMedidasProteccion);

            builder.Property(a => a.IdNoMedidasProteccion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.MedidasproteccionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdNoMedidasProteccion)
           .HasDefaultValueSql("newId()");
        }

    }
}
