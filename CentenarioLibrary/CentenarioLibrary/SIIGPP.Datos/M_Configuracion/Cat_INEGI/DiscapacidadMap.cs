using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class DiscapacidadMap : IEntityTypeConfiguration<Discapacidad>
    {
        public void Configure(EntityTypeBuilder<Discapacidad> builder)
        {
            builder.ToTable("C_DISCAPACIDAD")
                 .HasKey(a => a.IdDiscapacidad);
            builder
            .Property(a => a.IdDiscapacidad)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdDiscapacidad)
           .HasDefaultValueSql("newId()");
        }
    }
}
