using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PAtencion;
using System;
using System.Collections.Generic;
using System.Text;
namespace SIIGPP.Datos.M_Cat.PAtencion
{
    public class PreAtencionMap : IEntityTypeConfiguration<PreAtencion>
    {
        public void Configure(EntityTypeBuilder<PreAtencion> builder)
        {
            builder.ToTable("PRE_ATENCION")
                          .HasKey(a => a.IdPAtencion);
            builder.Property(a => a.IdPAtencion)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();


           builder.Property(a => a.PRegistroId)
          .HasColumnType("UNIQUEIDENTIFIER")
          .IsRequired();


            builder
          .Property(a => a.IdPAtencion)
          .HasDefaultValueSql("newId()");

            

        }
    }
}
