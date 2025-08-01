using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;


namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class TipoOjosMap : IEntityTypeConfiguration<TipoOjos>
    {
        public void Configure(EntityTypeBuilder<TipoOjos> builder)
        {
            builder.ToTable("C_TIPO_OJOS")
                .HasKey(a => a.IdTipoOjos);
            builder
           .Property(a => a.IdTipoOjos)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipoOjos)
           .HasDefaultValueSql("newId()");
        }

    }
}
