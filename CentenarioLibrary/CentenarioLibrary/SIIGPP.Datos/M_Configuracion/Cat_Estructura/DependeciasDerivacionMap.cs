using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Estructura
{
    public class DependeciasDerivacionMap : IEntityTypeConfiguration<DependeciasDerivacion>
    {
        public void Configure(EntityTypeBuilder<DependeciasDerivacion> builder)
        {
            builder.ToTable("C_DEPENDECIAS_DERIVACION")
                   .HasKey(a => a.IdDDerivacion);


            builder.Property(a => a.IdDDerivacion)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
            .Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();
            builder
           .Property(a => a.IdDDerivacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
