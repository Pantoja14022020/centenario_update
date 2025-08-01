using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.UAlmacenamiento;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.UAlmacenamiento
{
    public class AlmacenamientoMap : IEntityTypeConfiguration<Almacenamiento>
    {
        public void Configure(EntityTypeBuilder<Almacenamiento> builder)
        {
            builder.ToTable("CA_ALMACENAMIENTO")
           .HasKey(a => a.IdAlmacenamiento);

            builder.Property(a => a.IdAlmacenamiento)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdAlmacenamiento)
           .HasDefaultValueSql("newId()");
        }
    }
}
