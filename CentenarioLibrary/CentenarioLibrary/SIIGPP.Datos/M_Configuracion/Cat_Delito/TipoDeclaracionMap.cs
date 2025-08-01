using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
   public  class TipoDeclaracionMap : IEntityTypeConfiguration<TipoDeclaracion>
    {
        public void Configure(EntityTypeBuilder<TipoDeclaracion> builder)
        {
            builder.ToTable("CD_TIPODECLARACION")
                       .HasKey(a => a.IdTipoDeclaracion);
            builder
         .Property(a => a.IdTipoDeclaracion)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();
            builder
           .Property(a => a.IdTipoDeclaracion)
           .HasDefaultValueSql("newId()");
        }
    }
}
