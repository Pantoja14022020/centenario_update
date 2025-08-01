using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tipo_DentaduraMap : IEntityTypeConfiguration<Tipo_Dentadura>
    {
        public void Configure(EntityTypeBuilder<Tipo_Dentadura> builder)
        {
            builder.ToTable("C_TIPO_DENTADURA")
                .HasKey(a => a.IdTipo_Dentadura);

            builder
           .Property(a => a.IdTipo_Dentadura)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdTipo_Dentadura)
           .HasDefaultValueSql("newId()");
        }

    }
}
