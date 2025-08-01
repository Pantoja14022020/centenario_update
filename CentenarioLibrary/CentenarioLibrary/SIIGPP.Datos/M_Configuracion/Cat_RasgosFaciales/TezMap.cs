using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class TezMap : IEntityTypeConfiguration<Tez>
    {
        public void Configure(EntityTypeBuilder<Tez> builder)
        {
            builder.ToTable("C_TEZ")
                .HasKey(a => a.IdTez);
            builder
           .Property(a => a.IdTez)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTez)
           .HasDefaultValueSql("newId()");
        }

    }
}
