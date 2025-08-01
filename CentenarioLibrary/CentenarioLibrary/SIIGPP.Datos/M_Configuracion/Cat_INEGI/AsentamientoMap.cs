using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class AsentamientoMap : IEntityTypeConfiguration<Asentamiento>
    {
        public void Configure(EntityTypeBuilder<Asentamiento> builder)
        {
            builder.ToTable("C_TIPO_ASENTAMIENTO")
                 .HasKey(a => a.IdAsentamiento);

            builder
            .Property(a => a.IdAsentamiento)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdAsentamiento)
           .HasDefaultValueSql("newId()");
        }
    }
}
