using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;


namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tipo2OjosMap : IEntityTypeConfiguration<Tipo2Ojos>
    {
        public void Configure(EntityTypeBuilder<Tipo2Ojos> builder)
        {
            builder.ToTable("C_TIPO2_OJOS")
                .HasKey(a => a.IdTipodeOjos);
            builder
           .Property(a => a.IdTipodeOjos)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipodeOjos)
           .HasDefaultValueSql("newId()");
        }

    }
}
