using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Grosor_de_labiosMap : IEntityTypeConfiguration<Grosor_de_labios>
    {
        public void Configure(EntityTypeBuilder<Grosor_de_labios> builder)
        {
            builder.ToTable("C_GROSOR_DE_LABIOS")
                .HasKey(a => a.IdGrosorLabios);
            builder
           .Property(a => a.IdGrosorLabios)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdGrosorLabios)
           .HasDefaultValueSql("newId()");
        }

    }
}
