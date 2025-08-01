using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tipo_de_MentonMap : IEntityTypeConfiguration<Tipo_de_Menton>
    {
        public void Configure(EntityTypeBuilder<Tipo_de_Menton> builder)
        {
            builder.ToTable("C_TIPO_MENTON")
                .HasKey(a => a.IdTipo_de_Menton);

            builder
           .Property(a => a.IdTipo_de_Menton)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdTipo_de_Menton)
           .HasDefaultValueSql("newId()");
        }

    }
}
