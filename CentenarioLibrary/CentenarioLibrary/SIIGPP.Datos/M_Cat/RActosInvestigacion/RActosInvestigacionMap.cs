using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.RActosInvestigacion
{
    public class RActosInvestigacionMap : IEntityTypeConfiguration<RActoInvestigacion>
    {
        public void Configure(EntityTypeBuilder<RActoInvestigacion> builder)
        {
            builder.ToTable("CAT_RACTOSINVESTIGACION")
                    .HasKey(a => a.IdRActosInvestigacion);

            builder.Property(a => a.IdRActosInvestigacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRActosInvestigacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
