using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Punta_NarizMap : IEntityTypeConfiguration<Punta_Nariz>
    {
        public void Configure(EntityTypeBuilder<Punta_Nariz> builder)
        {
            builder.ToTable("C_PUNTA_NARIZ")
                .HasKey(a => a.IdPunta_Nariz);

            builder
           .Property(a => a.IdPunta_Nariz)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdPunta_Nariz)
           .HasDefaultValueSql("newId()");
        }

    }
}
