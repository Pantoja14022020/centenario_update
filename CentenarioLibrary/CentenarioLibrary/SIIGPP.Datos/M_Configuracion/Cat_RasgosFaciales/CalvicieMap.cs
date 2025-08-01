using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class CalvicieMap : IEntityTypeConfiguration<Calvicie>
    {
        public void Configure(EntityTypeBuilder<Calvicie> builder)
        {
            builder.ToTable("C_CALVICIE")
                .HasKey(a => a.IdCalvicie);

            builder
           .Property(a => a.IdCalvicie)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdCalvicie)
           .HasDefaultValueSql("newId()");
        }

    }
}
