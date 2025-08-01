using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class OcupacionMap : IEntityTypeConfiguration<Ocupacion>
    {
        public void Configure(EntityTypeBuilder<Ocupacion> builder)
        {
            builder.ToTable("C_OCUPACION")
                 .HasKey(a => a.IdOcupacion);
            builder
            .Property(a => a.IdOcupacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdOcupacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
