using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Direcciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Direcciones
{
    public class DireccionPersonalMap : IEntityTypeConfiguration<DireccionPersonal>
    {
        public void Configure(EntityTypeBuilder<DireccionPersonal> builder)
        {
            builder.ToTable("CAT_DIRECCION_PERSONAL")
                   .HasKey(a => a.IdDPersonal);
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
            builder.Property(a => a.Referencia)
                   .HasColumnType("nvarchar(300)"); 
            builder.Property(a => a.Pais)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.Estado)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.Municipio)
                   .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Localidad)
                   .HasColumnType("nvarchar(200)");
            builder.Property(a => a.CP)
                   .HasColumnType("int");

            builder.Property(a => a.IdDPersonal)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDPersonal)
           .HasDefaultValueSql("newId()");

        }
    }
}
