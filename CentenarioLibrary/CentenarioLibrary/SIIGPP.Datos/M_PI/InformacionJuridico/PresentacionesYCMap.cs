using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class PresentacionesYCMap : IEntityTypeConfiguration<PresentacionesYC>
    {
        public void Configure(EntityTypeBuilder<PresentacionesYC> builder)
        {
            builder.ToTable("PI_IJ_PRESENTACIONES_Y_C")
                    .HasKey(a => a.IdPresentacionesYC);

            builder.Property(a => a.IdPresentacionesYC)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdPresentacionesYC)
           .HasDefaultValueSql("newId()");
        }
    }
}
