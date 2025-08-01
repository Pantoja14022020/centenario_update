using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class PomulosMap : IEntityTypeConfiguration<Pomulos>
    {
        public void Configure(EntityTypeBuilder<Pomulos> builder)
        {
            builder.ToTable("C_POMULOS")
                .HasKey(a => a.IdPomulos);

            builder
           .Property(a => a.IdPomulos)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdPomulos)
           .HasDefaultValueSql("newId()");
        }

    }
}
