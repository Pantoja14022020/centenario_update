using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.DocumentacionSistema;


namespace SIIGPP.Datos.M_Configuracion.DocumentacionSistema
{
    public class ActualizacionesMap : IEntityTypeConfiguration<Actualizaciones>
    {
        public void Configure(EntityTypeBuilder<Actualizaciones> builder)
        {
            builder.ToTable("DOCS_ACTUALIZACIONES")
                .HasKey(a => a.IdActualizacion);
            builder
           .Property(a => a.IdActualizacion)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdActualizacion)
           .HasDefaultValueSql("newId()");
        }

    }
}
