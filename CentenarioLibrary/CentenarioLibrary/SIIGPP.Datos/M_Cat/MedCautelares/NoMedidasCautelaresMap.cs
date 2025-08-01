using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedCautelares;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedCautelares
{
    public class NoMedidasCautelaresMap : IEntityTypeConfiguration<NoMedidasCautelares>
    {
        public void Configure(EntityTypeBuilder<NoMedidasCautelares> builder)
        {
            builder.ToTable("CAT_NO_MEDIDASCAUTELARES")
                 .HasKey(a => a.IdNoMedidasCautelares);


            builder.Property(a => a.IdNoMedidasCautelares)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.MedidasCautelaresId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdNoMedidasCautelares)
           .HasDefaultValueSql("newId()");
        }
    }
}
