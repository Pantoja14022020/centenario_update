using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedAfiliacion
{
    public class MediaAfiliacionMap : IEntityTypeConfiguration<MediaAfiliacion>
    {
        public void Configure(EntityTypeBuilder<MediaAfiliacion> builder)
        {
            builder.ToTable("CAT_MEDIAAFILIACION")
                  .HasKey(a => a.idMediaAfiliacion); 
            builder.Property(a => a.FechaSys)
                 .HasColumnType("DateTime");

            builder.Property(a => a.idMediaAfiliacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.idMediaAfiliacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
