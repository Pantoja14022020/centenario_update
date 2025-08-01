using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.GRAC;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.GRAC
{
    public class RacMap : IEntityTypeConfiguration<Rac>
    {
        public void Configure(EntityTypeBuilder<Rac> builder)
        {
            builder.ToTable("RAC")
                          .HasKey(a => a.idRac);

            builder.Property(a => a.idRac)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.AgenciaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.idRac)
           .HasDefaultValueSql("newId()");
        }
    }
}
