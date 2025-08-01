using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
    public class ClasificaOrdenResultMap : IEntityTypeConfiguration<ClasificaOrdenResult>
    {
        public void Configure(EntityTypeBuilder<ClasificaOrdenResult> builder)
        {
            builder.ToTable("CD_CLAORDRES")
                   .HasKey(a => a.IdClasificaOrdenResult);
            builder
          .Property(a => a.IdClasificaOrdenResult)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdClasificaOrdenResult)
           .HasDefaultValueSql("newId()");
        }
    }
}
