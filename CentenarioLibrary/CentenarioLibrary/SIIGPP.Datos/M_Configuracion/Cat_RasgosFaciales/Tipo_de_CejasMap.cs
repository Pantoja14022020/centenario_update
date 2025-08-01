using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tipo_de_CejasMap : IEntityTypeConfiguration<Tipo_de_Cejas>
    {
        public void Configure(EntityTypeBuilder<Tipo_de_Cejas> builder)
        {
            builder.ToTable("C_FORMA_DE_CEJAS")
                .HasKey(a => a.IdFormaCejas);
            builder
           .Property(a => a.IdFormaCejas)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdFormaCejas)
           .HasDefaultValueSql("newId()");
        }

    }
}
