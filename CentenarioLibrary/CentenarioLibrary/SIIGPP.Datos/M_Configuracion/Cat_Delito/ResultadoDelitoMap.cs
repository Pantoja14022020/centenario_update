using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
    public class ResultadoDelitoMap : IEntityTypeConfiguration<ResultadoDelito>
    {
        public void Configure(EntityTypeBuilder<ResultadoDelito> builder)
        {
            builder.ToTable("CD_RESULTADODELITO")
                   .HasKey(a => a.IdResultadoDelito);
            builder
         .Property(a => a.IdResultadoDelito)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();
            builder
           .Property(a => a.IdResultadoDelito)
           .HasDefaultValueSql("newId()");
        }
    }
}
