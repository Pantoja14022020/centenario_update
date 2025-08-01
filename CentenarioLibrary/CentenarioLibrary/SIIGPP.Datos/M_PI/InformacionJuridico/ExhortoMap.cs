using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class ExhortoMap : IEntityTypeConfiguration<Exhorto>
    {
        public void Configure(EntityTypeBuilder<Exhorto> builder)
        {
            builder.ToTable("PI_IJ_EXHORTO")
                    .HasKey(a => a.IdExhorto);

            builder.Property(a => a.IdExhorto)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdExhorto)
           .HasDefaultValueSql("newId()");
        }
    }
}
