using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class ComparecenciasElementosMap : IEntityTypeConfiguration<ComparecenciasElementos>
    {
        public void Configure(EntityTypeBuilder<ComparecenciasElementos> builder)
        {
            builder.ToTable("PI_IJ_COMPARECENCIA_ELEMENTOS")
                    .HasKey(a => a.IdCompElementos);

            builder.Property(a => a.IdCompElementos)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdCompElementos)
           .HasDefaultValueSql("newId()");
        }
    }
}
