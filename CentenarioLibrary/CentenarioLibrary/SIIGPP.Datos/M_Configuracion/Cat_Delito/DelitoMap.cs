using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
    public class DelitoMap : IEntityTypeConfiguration<Delito>
    {
        public void Configure(EntityTypeBuilder<Delito> builder)
        {
            builder.ToTable("C_DELITO")
            .HasKey(a => a.IdDelito);
            builder
         .Property(a => a.IdDelito)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();
            builder
           .Property(a => a.IdDelito)
           .HasDefaultValueSql("newId()");
        }
    }
}
