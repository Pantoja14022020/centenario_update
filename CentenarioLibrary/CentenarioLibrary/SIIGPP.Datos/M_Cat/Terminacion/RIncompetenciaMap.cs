using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Terminacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Terminacion
{
    public class RIncompetenciaMap : IEntityTypeConfiguration<RInconpentencia>
    {
        public void Configure(EntityTypeBuilder<RInconpentencia> builder)
        {
            builder.ToTable("CAT_INCOMPETENCIA")
                    .HasKey(a => a.IdInconpentencia);

            builder.Property(a => a.IdInconpentencia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdInconpentencia)
           .HasDefaultValueSql("newId()");
        }
    }
}