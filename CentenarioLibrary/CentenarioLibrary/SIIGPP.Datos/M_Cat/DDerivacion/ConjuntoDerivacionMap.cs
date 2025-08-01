using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.DDerivacion
{
    public class ConjuntoDerivacionMap : IEntityTypeConfiguration<ConjuntoDerivaciones>
    {
        public void Configure(EntityTypeBuilder<ConjuntoDerivaciones> builder)
        {
            builder.ToTable("JR_CONJUNTODERIVACIONES")
                 .HasKey(a => a.IdConjuntoDerivaciones);

            builder.Property(a => a.IdConjuntoDerivaciones)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();


            builder.Property(a => a.EnvioId)
                 .HasColumnType("UNIQUEIDENTIFIER");

            builder
           .Property(a => a.IdConjuntoDerivaciones)
           .HasDefaultValueSql("newId()");


        }
    }
}
