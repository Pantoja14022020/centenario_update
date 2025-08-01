using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
    public class IntensionDelitoMap : IEntityTypeConfiguration<IntensionDelito>
    {
        public void Configure(EntityTypeBuilder<IntensionDelito> builder)
        {
            builder.ToTable("CD_INTESIONDELITO")
                     .HasKey(a => a.IdIntesionDelio);
            builder
         .Property(a => a.IdIntesionDelio)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();
            builder
           .Property(a => a.IdIntesionDelio)
           .HasDefaultValueSql("newId()");
        }
    }
}
