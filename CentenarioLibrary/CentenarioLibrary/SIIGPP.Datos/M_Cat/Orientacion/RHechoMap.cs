using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Orientacion
{
    public class RHechoMap : IEntityTypeConfiguration<RHecho>

    {
        public void Configure(EntityTypeBuilder<RHecho> builder)
        {
            builder.ToTable("CAT_RHECHO")
                .HasKey(a => a.IdRHecho);
            builder.Property(a => a.FechaElevaNuc)
                   .HasColumnType("DateTime");
            builder.Property(a => a.FechaHoraSuceso)
                 .HasColumnType("DateTime");

            builder.Property(a => a.IdRHecho)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RAtencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.Agenciaid)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.NucId)
            .HasColumnType("UNIQUEIDENTIFIER");

            builder
           .Property(a => a.IdRHecho)
           .HasDefaultValueSql("newId()");

        }
    }
}
