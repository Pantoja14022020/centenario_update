using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Arma;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_Cat.Arma
{
    public class RArmaMap : IEntityTypeConfiguration<RArma>
    {
        public void Configure(EntityTypeBuilder<RArma> builder)
        {
            builder.ToTable("CAT_ARMA")
                .HasKey(a => a.IdRarma);

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

            builder.Property(a => a.IdRarma)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRarma)
           .HasDefaultValueSql("newId()");

        }

    }
}
