using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class ArrestoMap : IEntityTypeConfiguration<Arresto>
    {
        public void Configure(EntityTypeBuilder<Arresto> builder)
        {
            builder.ToTable("PI_IJ_ARRESTO")
                    .HasKey(a => a.IdArresto);

            builder.Property(a => a.IdArresto)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdArresto)
           .HasDefaultValueSql("newId()");
        }
    }
}
