using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.Detenciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.Detenciones
{
    public class DetencionMap : IEntityTypeConfiguration<Detencion>
    {
        public void Configure(EntityTypeBuilder<Detencion> builder)
        {
            builder.ToTable("PI_DETENCION")
                    .HasKey(a => a.IdDetencion);

            builder.Property(a => a.IdDetencion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDetencion)
           .HasDefaultValueSql("newId()");
        }
    }
}
