using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Configuracion
{
    public class ClasificacionPersonaMap : IEntityTypeConfiguration<ClasificacionPersona>
    {
        public void Configure(EntityTypeBuilder<ClasificacionPersona> builder)
        {
            builder.ToTable("C_CLASIFICACIONPERSONA")
                  .HasKey(a => a.IdClasificacionPersona);
            builder
          .Property(a => a.IdClasificacionPersona)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdClasificacionPersona)
           .HasDefaultValueSql("newId()");
        }
    }
}
