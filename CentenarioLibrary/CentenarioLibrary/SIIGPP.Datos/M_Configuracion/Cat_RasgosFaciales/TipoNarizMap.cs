using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class TipoNarizMap : IEntityTypeConfiguration<TipoNariz>
    {
        public void Configure(EntityTypeBuilder<TipoNariz> builder)
        {
            builder.ToTable("C_TIPO_DE_NARIZ")
                .HasKey(a => a.IdTipoNariz);
            builder
           .Property(a => a.IdTipoNariz)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdTipoNariz)
           .HasDefaultValueSql("newId()");
        }

    }
}
