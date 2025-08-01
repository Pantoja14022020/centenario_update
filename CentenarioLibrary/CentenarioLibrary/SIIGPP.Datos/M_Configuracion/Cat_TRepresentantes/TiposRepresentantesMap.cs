using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_TRepresentantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_TRepresentantes
{
    public class TiposRepresentantesMap : IEntityTypeConfiguration<TiposRepresentantes>
    {
        public void Configure(EntityTypeBuilder<TiposRepresentantes> builder)
        {
            builder.ToTable("C_TIPOSREPRESENTANTES")
                .HasKey(a => a.IdTipoRepresentantes);
            builder
           .Property(a => a.IdTipoRepresentantes)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipoRepresentantes)
           .HasDefaultValueSql("newId()");
        }
    }
}
