using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tipo_de_OrejasMap : IEntityTypeConfiguration<Tipo_de_Orejas>
    {
        public void Configure(EntityTypeBuilder<Tipo_de_Orejas> builder)
        {
            builder.ToTable("C_TIPO_DE_OREJAS")
                .HasKey(a => a.IdTipoOrejas);
            builder
           .Property(a => a.IdTipoOrejas)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipoOrejas)
           .HasDefaultValueSql("newId()");
        }

    }
}
