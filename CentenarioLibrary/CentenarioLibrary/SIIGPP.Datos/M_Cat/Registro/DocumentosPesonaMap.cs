using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Registro
{
    public class DocumentosPesonaMap : IEntityTypeConfiguration<DocumentosPesona>
    {
        public void Configure(EntityTypeBuilder<DocumentosPesona> builder)
        {
            builder.ToTable("CAT_DOCSPERSONA")
               .HasKey(a => a.IdDocumentoPersona);
            builder.Property(a => a.FechaRegistro)
                   .HasColumnType("DateTime");

            builder.Property(a => a.IdDocumentoPersona)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDocumentoPersona)
           .HasDefaultValueSql("newId()");
        }
    }
}
