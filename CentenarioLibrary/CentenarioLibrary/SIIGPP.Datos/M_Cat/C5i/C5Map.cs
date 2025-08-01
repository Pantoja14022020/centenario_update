using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.C5i;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.C5i
{
    public class C5Map : IEntityTypeConfiguration<C5>
    {
        public void Configure(EntityTypeBuilder<C5> builder)
        {
            builder.ToTable("CAT_C5FORMATOS")
                .HasKey(a => a.IdC5);

            builder.Property(a => a.IdC5)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdC5)
           .HasDefaultValueSql("newId()");

        }
    }
}
