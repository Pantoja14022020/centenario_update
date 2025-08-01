using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Representantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Representantes
{
    public class DocsRepresentantesMap : IEntityTypeConfiguration<DocsRepresentantes>
    {
        public void Configure(EntityTypeBuilder<DocsRepresentantes> builder)
        {
            builder.ToTable("CAT_DOCSREPRESENTANTES")
                    .HasKey(a => a.IdDocsRepresentantes);

            builder.Property(a => a.IdDocsRepresentantes)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RepresentanteId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDocsRepresentantes)
           .HasDefaultValueSql("newId()");
        }
    }
}
