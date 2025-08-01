using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.CArchivos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.CArchivo

{
    public class ArchivosMAP : IEntityTypeConfiguration<Archivos>
    {
        public void Configure(EntityTypeBuilder<Archivos> builder)
        {
            builder.ToTable("CAT_ARCHIVOS")
                    .HasKey(a => a.IdArchivos);

            builder.Property(a => a.IdArchivos)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdArchivos)
           .HasDefaultValueSql("newId()");
        }
    }
}
