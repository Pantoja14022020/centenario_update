using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_ConfiGlobal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_ConfGlobal
{
    public class ConfGlobalMap : IEntityTypeConfiguration<ConfGlobal>
    {
        public void Configure(EntityTypeBuilder<ConfGlobal> builder)
        {
            builder.ToTable("C_CONFGLOBAL")
                   .HasKey(a => a.IdConfGlobal);
            builder
          .Property(a => a.IdConfGlobal)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdConfGlobal)
           .HasDefaultValueSql("newId()");
        }
    }
}
