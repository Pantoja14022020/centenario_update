using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class RequerimientosVariosMap : IEntityTypeConfiguration<RequerimientosVarios>
    {
        public void Configure(EntityTypeBuilder<RequerimientosVarios> builder)
        {
            builder.ToTable("PI_IJ_REQUERIMIENTOS_VARIOS")
                    .HasKey(a => a.IdReqVarios);

            builder.Property(a => a.IdReqVarios)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdReqVarios)
           .HasDefaultValueSql("newId()");
        }
    }
}
