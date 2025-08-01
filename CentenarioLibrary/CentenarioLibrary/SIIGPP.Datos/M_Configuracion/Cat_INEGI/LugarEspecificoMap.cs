using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class LugarEspecificoMap : IEntityTypeConfiguration<LugarEspecifico>
    {
        public void Configure(EntityTypeBuilder<LugarEspecifico> builder)
        {
            builder.ToTable("C_LUGAR_ESPECIFICO")
                 .HasKey(a => a.IdLugarEspecifico);

            builder
            .Property(a => a.IdLugarEspecifico)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdLugarEspecifico)
           .HasDefaultValueSql("newId()");
        }
    }
}
