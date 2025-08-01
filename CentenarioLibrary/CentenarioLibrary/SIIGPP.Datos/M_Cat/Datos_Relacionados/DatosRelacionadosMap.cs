using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Datos_Relacionados;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Datos_Relacionados
{
    public class DatosRelacionadosMap : IEntityTypeConfiguration<DatosRelacionados>
    {
        public void Configure(EntityTypeBuilder<DatosRelacionados> builder)
        {
            builder.ToTable("CAT_DATOSRELACIONADOS")
                    .HasKey(a => a.IdDatosRelacionados);

            builder.Property(a => a.IdDatosRelacionados)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL");

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDatosRelacionados)
           .HasDefaultValueSql("newId()");
        }
    }
}

