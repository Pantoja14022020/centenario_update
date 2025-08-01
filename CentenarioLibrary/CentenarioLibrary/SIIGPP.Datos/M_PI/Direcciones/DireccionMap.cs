using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.Direcciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.Direcciones
{
    public class DireccionMap : IEntityTypeConfiguration<Direccion>
    {
        public void Configure(EntityTypeBuilder<Direccion> builder)
        {
            builder.ToTable("PI_DIRECCION")
                    .HasKey(a => a.IdDireccion);

            builder.Property(a => a.IdDireccion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PIPersonaVisitaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDireccion)
           .HasDefaultValueSql("newId()");
        }
    }
}
