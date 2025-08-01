using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Indicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Indicios
{
    public class IndiciosMap : IEntityTypeConfiguration<Indicios>
    {
        public void Configure(EntityTypeBuilder<Indicios> builder)
        {
            builder.ToTable("CAT_INDICIOS")
                .HasKey(a => a.IdIndicio);

            builder.Property(a => a.Puesto)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Usuario)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Subproc)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Agencia)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Distrito)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.IdIndicio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdIndicio)
           .HasDefaultValueSql("newId()");

        }
    }
}
