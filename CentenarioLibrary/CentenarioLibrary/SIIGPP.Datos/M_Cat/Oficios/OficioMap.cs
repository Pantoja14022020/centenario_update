using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Oficios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Oficios
{
    public class OficioMap : IEntityTypeConfiguration<Oficio>
    {
        public void Configure(EntityTypeBuilder<Oficio> builder)
        {
            builder.ToTable("CAT_OFICIO")
                    .HasKey(a => a.IdOficios);

            builder.Property(a => a.IdOficios)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdOficios)
           .HasDefaultValueSql("newId()");
        }
    }
}
