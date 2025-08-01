using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.ContencionesPersonas;

namespace SIIGPP.Datos.M_Cat.ContencionesPersonas
{
    public class ContencionesPersonaMap : IEntityTypeConfiguration<ContencionesPersona>
    {
        public void Configure(EntityTypeBuilder<ContencionesPersona> builder)
        {
            builder.ToTable("CAT_CONTENCIONES_PERSONAS")
                    .HasKey(a => a.IdContencionesPersona);

            builder.Property(a => a.IdContencionesPersona)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RAtencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdContencionesPersona)
           .HasDefaultValueSql("newId()");
        }
    }
}
