using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Diligencias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Diligencias
{
    public class RDiligenciasForaneasMap : IEntityTypeConfiguration<RDiligenciasForaneas>
    {
        public void Configure(EntityTypeBuilder<RDiligenciasForaneas> builder)
        {
            builder.ToTable("CAT_RDILIGENCIASFORANEAS")
                     .HasKey(a => a.IdRDiligenciasForaneas);

            builder.Property(a => a.IdRDiligenciasForaneas)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ASPId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();


            builder
           .Property(a => a.IdRDiligenciasForaneas)
           .HasDefaultValueSql("newId()");
        }
    }
}
