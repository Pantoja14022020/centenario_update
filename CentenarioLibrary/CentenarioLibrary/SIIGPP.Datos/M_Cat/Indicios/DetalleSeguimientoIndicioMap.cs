using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Indicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Indicios
{
    public class DetalleSeguimientoIndicioMap : IEntityTypeConfiguration<DetalleSeguimientoIndicio>
    {
        public void Configure(EntityTypeBuilder<DetalleSeguimientoIndicio> builder)
        {
            builder.ToTable("CAT_DETALLE_SEGUIMIENTO")
                .HasKey(a => a.IdDetalles);

            builder.Property(a => a.OrigenLugar)
                .HasColumnType("nvarchar(200)");

            builder.Property(a => a.QuienEntrega)
                .HasColumnType("nvarchar(200)");

            builder.Property(a => a.QuienRecibe)
                .HasColumnType("nvarchar(200)");

            builder.Property(a => a.IdDetalles)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.IndiciosId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDetalles)
           .HasDefaultValueSql("newId()");

        }

    }
}
