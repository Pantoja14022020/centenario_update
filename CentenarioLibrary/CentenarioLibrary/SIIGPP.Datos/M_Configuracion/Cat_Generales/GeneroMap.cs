using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Generales
{
    public class GeneroMap : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("C_GENERO")
                    .HasKey(a => a.IdGenero);
            builder
            .Property(a => a.IdGenero)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdGenero)
           .HasDefaultValueSql("newId()");
        }
    }
}
