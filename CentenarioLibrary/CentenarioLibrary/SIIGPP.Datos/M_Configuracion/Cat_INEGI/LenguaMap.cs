using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class LenguaMap: IEntityTypeConfiguration<Lengua>
    {
      

        public void Configure(EntityTypeBuilder<Lengua> builder)
        {
            builder.ToTable("C_LENGUA")
              .HasKey(a => a.IdLengua);
            builder
            .Property(a => a.IdLengua)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdLengua)
           .HasDefaultValueSql("newId()");
        }
    }
}
