using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Terminacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Terminacion
{
    public class DeterminacionArchivoMap : IEntityTypeConfiguration<DeterminacionArchivo>
    {
        public void Configure(EntityTypeBuilder<DeterminacionArchivo> builder)
        {
            builder.ToTable("CAT_TERMINACIONARCHIVO")
                    .HasKey(a => a.IdDeterminacionArchivo);

            builder.Property(a => a.IdDeterminacionArchivo)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDeterminacionArchivo)
           .HasDefaultValueSql("newId()");
        }
    }
}
