using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Indicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Indicios
{
    public class TipoIndicioMap : IEntityTypeConfiguration<TipoIndicio>
    {
        public void Configure(EntityTypeBuilder<TipoIndicio> builder)
        {
            builder.ToTable("CI_Tipo")
                .HasKey(a => a.IdTipoIndicio);

            builder
            .Property(a => a.IdTipoIndicio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdTipoIndicio)
           .HasDefaultValueSql("newId()");
        }

    }
}
