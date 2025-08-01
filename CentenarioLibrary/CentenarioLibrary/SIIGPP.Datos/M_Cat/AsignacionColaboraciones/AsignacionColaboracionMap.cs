using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.AsignacionColaboraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.AsignacionColaboraciones
{
    public class AsignacionColaboracionMap : IEntityTypeConfiguration<AsignacionColaboracion>
    {
        public void Configure(EntityTypeBuilder<AsignacionColaboracion> builder)
        {
            builder.ToTable("CAT_ASIGNACION_COLABORACION")
                    .HasKey(a => a.IdAsignacionColaboraciones);

            builder.Property(a => a.IdAsignacionColaboraciones)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();
            builder.Property(a => a.SColaboracionMPId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdAsignacionColaboraciones)
           .HasDefaultValueSql("newId()");
        }
    }
}
