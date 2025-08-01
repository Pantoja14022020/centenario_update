using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Forma_de_CaraMap : IEntityTypeConfiguration<Forma_de_Cara>
    {
        public void Configure(EntityTypeBuilder<Forma_de_Cara> builder)
        {
            builder.ToTable("C_FORMA_DE_CARA")
                .HasKey(a => a.IdFormaCara);
            builder
           .Property(a => a.IdFormaCara)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdFormaCara)
           .HasDefaultValueSql("newId()");
        }

    }
}
