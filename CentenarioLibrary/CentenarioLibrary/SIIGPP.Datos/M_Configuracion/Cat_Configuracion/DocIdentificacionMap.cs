using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Configuracion
{
    public class DocIdentificacionMap : IEntityTypeConfiguration<DocIdentificacion>
    {
        public void Configure(EntityTypeBuilder<DocIdentificacion> builder)
        {
            builder.ToTable("C_DOCIDENTIFICACION")
                 .HasKey(a => a.IdDocIdentificacion);
            builder
          .Property(a => a.IdDocIdentificacion)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdDocIdentificacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
