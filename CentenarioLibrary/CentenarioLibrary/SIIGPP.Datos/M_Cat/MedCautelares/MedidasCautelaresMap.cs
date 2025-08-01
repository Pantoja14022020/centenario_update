using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedCautelares;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedCautelares
{
    public class MedidasCautelaresMap : IEntityTypeConfiguration<MedidasCautelares>
    {
        public void Configure(EntityTypeBuilder<MedidasCautelares> builder)
        {
            builder.ToTable("CAT_MEDIDASCAUTELARES")
                 .HasKey(a => a.IdMedCautelares);
            builder.Property(a => a.FechaSys)
                 .HasColumnType("DateTime");

            builder.Property(a => a.IdMedCautelares)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdMedCautelares)
           .HasDefaultValueSql("newId()");
        }
    }
}
