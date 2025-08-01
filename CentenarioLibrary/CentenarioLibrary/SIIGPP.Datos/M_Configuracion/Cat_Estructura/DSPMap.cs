using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Estructura
{
    public class DSPMap : IEntityTypeConfiguration<DSP>
    {
        public void Configure(EntityTypeBuilder<DSP> builder)
        {
            builder.ToTable("C_DSP")
                   .HasKey(a => a.IdDSP);
            builder
         .Property(a => a.IdDSP)
         .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
         .IsRequired();
            builder
            .Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();
            builder
           .Property(a => a.IdDSP)
           .HasDefaultValueSql("newId()");

        }
    }
}
