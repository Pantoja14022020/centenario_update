using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.GNUC;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.GNUC
{
    public class NucMap : IEntityTypeConfiguration<Nuc>
    {
        public void Configure(EntityTypeBuilder<Nuc> builder)
        {
            builder.ToTable("NUC")
                    .HasKey(a => a.idNuc);

            builder.Property(a => a.idNuc)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.AgenciaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.idNuc)
           .HasDefaultValueSql("newId()");
        }
    }
}
