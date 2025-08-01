using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Direcciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Direcciones
{
    public class DireccionEscuchaMap : IEntityTypeConfiguration<DireccionEscucha>
    {
        public void Configure(EntityTypeBuilder<DireccionEscucha> builder)
        {
            builder.ToTable("CAT_DIRECCION_ESCUCHA")
                       .HasKey(a => a.IdDEscucha);

            builder.Property(a => a.Calle)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.NoInt)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.NoExt)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.EntreCalle1)
                   .HasColumnType("nvarchar(200)");
            builder.Property(a => a.EntreCalle2)
                   .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Pais)
                   .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Estado)
               .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Municipio)
               .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Localidad)
                .HasColumnType("nvarchar(200)");

            builder.Property(a => a.IdDEscucha)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RAPId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDEscucha)
           .HasDefaultValueSql("newId()");
        }
    }
}
