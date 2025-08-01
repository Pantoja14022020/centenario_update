using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_JuzgadoAgencias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_JuzgadosAgencia
{
    public class JuzgadosAgenciasMap : IEntityTypeConfiguration<JuzgadosAgencias>
    {
        public void Configure(EntityTypeBuilder<JuzgadosAgencias> builder)
        {
            builder.ToTable("C_JUZGADOS_AGENCIAS")
                    .HasKey(a => a.IdJuzgadoAgencia);
            builder
           .Property(a => a.IdJuzgadoAgencia)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
            .Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();
            builder
           .Property(a => a.IdJuzgadoAgencia)
           .HasDefaultValueSql("newId()");
        }
    }
}
