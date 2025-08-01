using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_Cat.TipoViolencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.TViolencia
{
    public class TViolenciaMap : IEntityTypeConfiguration<TipoViolencia>
    {
        public void Configure(EntityTypeBuilder<TipoViolencia> builder)
        {
            builder.ToTable("C_TIPOVIOLENCIA")
                    .HasKey(a => a.IdTipoViolencia);

            builder.Property(a => a.IdTipoViolencia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdTipoViolencia)
           .HasDefaultValueSql("newId()");
        }
    }
}
