using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
    public class TipoMap : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.ToTable("CD_TIPO")
                         .HasKey(a => a.IdTipo);
            builder
         .Property(a => a.IdTipo)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();

            builder
            .Property(a => a.IdTipo)
            .HasDefaultValueSql("newId()");
        }
    }
}
