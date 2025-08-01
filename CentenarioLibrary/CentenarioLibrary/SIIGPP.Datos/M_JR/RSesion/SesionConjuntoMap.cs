using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RSesion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RSesion
{
    public class SesionConjuntoMap : IEntityTypeConfiguration<SesionConjunto>
    {
        public void Configure(EntityTypeBuilder<SesionConjunto> builder)
        {
            builder.ToTable("JR_SESIONESCONJUNTOS")
                .HasKey(a => a.IdSC);

            builder.Property(a => a.IdSC)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.SesionId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();
            builder.Property(a => a.ConjuntoDerivacionesId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdSC)
           .HasDefaultValueSql("newId()");
          

        }
    }
}
