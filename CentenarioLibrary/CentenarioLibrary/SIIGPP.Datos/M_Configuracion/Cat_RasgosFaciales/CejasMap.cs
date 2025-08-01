using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class CejasMap : IEntityTypeConfiguration<Cejas>
    {
        public void Configure(EntityTypeBuilder<Cejas> builder)
        {
            builder.ToTable("C_TIPO_DE_CEJAS")
                .HasKey(a => a.IdTipoCejas);
            builder
           .Property(a => a.IdTipoCejas)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipoCejas)
           .HasDefaultValueSql("newId()");
        }

    }
}
