using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Turnador;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Turnador
{
    public class AmpoTurnoMap : IEntityTypeConfiguration<AmpoTurno>
    {
        public void Configure(EntityTypeBuilder<AmpoTurno> builder)
        {
            builder.ToTable("CAT_AMPOTURNO")
                    .HasKey(a => a.IdAmpoTurno);

            builder.Property(a => a.IdAmpoTurno)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER");

            builder.Property(a => a.TurnoId)
            .HasColumnType("UNIQUEIDENTIFIER");

            builder
           .Property(a => a.IdAmpoTurno)
           .HasDefaultValueSql("newId()");
        }
    }
}
