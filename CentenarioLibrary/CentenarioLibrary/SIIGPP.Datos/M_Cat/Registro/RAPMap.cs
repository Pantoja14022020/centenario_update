using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Registro
{
    public class RAPMap : IEntityTypeConfiguration<RAP>
    {
        public void Configure(EntityTypeBuilder<RAP> builder)
        {
            builder.ToTable("CAT_RAP")
                   .HasKey(a => a.IdRAP);
            builder.Property(a => a.ClasificacionPersona)
                    .HasColumnType("nvarchar(200)");

            builder.Property(a => a.IdRAP)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RAtencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRAP)
           .HasDefaultValueSql("newId()");
        }
    }
}
