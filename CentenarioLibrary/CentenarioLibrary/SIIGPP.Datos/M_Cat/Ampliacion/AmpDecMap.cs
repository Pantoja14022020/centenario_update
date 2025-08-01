using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Ampliacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Ampliacion
{
    public class AmpDecMap : IEntityTypeConfiguration<AmpDec>
    {
        public void Configure(EntityTypeBuilder<AmpDec> builder)
        {
            builder.ToTable("CAT_AMPDEC")
               .HasKey(a => a.idAmpliacion);

            builder.Property(a => a.Fechasys)
                   .HasColumnType("DateTime");

            builder.Property(a => a.idAmpliacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.HechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.idAmpliacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
