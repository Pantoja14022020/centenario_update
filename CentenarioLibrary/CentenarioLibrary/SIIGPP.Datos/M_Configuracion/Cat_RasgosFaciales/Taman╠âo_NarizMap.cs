using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tamaño_NarizMap : IEntityTypeConfiguration<Tamaño_Nariz>
    {
        public void Configure(EntityTypeBuilder<Tamaño_Nariz> builder)
        {
            builder.ToTable("C_TAMAÑO_NARIZ")
                .HasKey(a => a.IdTamañoNariz);
            builder
           .Property(a => a.IdTamañoNariz)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTamañoNariz)
           .HasDefaultValueSql("newId()");
        }

    }
}
