using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Representantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Representantes
{
    public class RepresentanteMap : IEntityTypeConfiguration<Representante>
    {
        public void Configure(EntityTypeBuilder<Representante> builder)
        {
            builder.ToTable("CAT_REPRESENTANTES")
                    .HasKey(a => a.IdRepresentante);

            builder.Property(a => a.IdRepresentante)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRepresentante)
           .HasDefaultValueSql("newId()");
        }
    }
}
