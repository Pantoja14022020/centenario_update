using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedidasProteccion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedidasProteccion
{
    public class MedidasProteccionMap : IEntityTypeConfiguration<Medidasproteccion>
    {
        public void Configure(EntityTypeBuilder<Medidasproteccion> builder)
        {
            builder.ToTable("CAT_MEDIDASPROTECCION")
                .HasKey(a => a.IdMProteccion);

            builder.Property(a => a.Puesto)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Usuario)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Subproc)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.UAgencia)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Distrito)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.IdMProteccion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdMProteccion)
           .HasDefaultValueSql("newId()");
        }

    }
}
