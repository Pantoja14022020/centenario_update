using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito 
{
    public class TipoFueroMap : IEntityTypeConfiguration<TipoFuero>
    {
        public void Configure(EntityTypeBuilder<TipoFuero> builder)
        {
            builder.ToTable("CD_TIPOFUERO")
                     .HasKey(a => a.IdTipoFuero);
            builder
         .Property(a => a.IdTipoFuero)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();
            builder
           .Property(a => a.IdTipoFuero)
           .HasDefaultValueSql("newId()");
        }
    }
}
