using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Forma_de_MentonMap : IEntityTypeConfiguration<Forma_de_Menton>
    {
        public void Configure(EntityTypeBuilder<Forma_de_Menton> builder)
        {
            builder.ToTable("C_FORMA_DE_MENTON")
                .HasKey(a => a.IdFormaMenton);
            builder
           .Property(a => a.IdFormaMenton)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdFormaMenton)
           .HasDefaultValueSql("newId()");
        }

    }
}
