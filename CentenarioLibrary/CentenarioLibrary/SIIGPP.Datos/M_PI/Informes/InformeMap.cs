using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.Informes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_PI.Informes
{
    public class InformeMap : IEntityTypeConfiguration<Informe>
    {
        public void Configure(EntityTypeBuilder<Informe> builder)
        {
            builder.ToTable("PI_INFORMES")
                .HasKey(a => a.IdInforme);

            builder.Property(a => a.IdInforme)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PeritoAsignadoPIId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();


            builder
           .Property(a => a.IdInforme)
           .HasDefaultValueSql("newId()");
        }

    }
}
