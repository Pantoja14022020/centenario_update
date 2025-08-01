using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Bitacora;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Bitacora
{
    public class RbitacoraMap : IEntityTypeConfiguration<RBitacora>
    {
        public void Configure(EntityTypeBuilder<RBitacora> builder)
        {
            builder.ToTable("CAT_BITACORA")
                .HasKey(a => a.IdBitacora);
            builder.Property(a => a.Fechasis)
                   .HasColumnType("DateTime");

            builder.Property(a => a.IdBitacora)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.IdPersona)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.rHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdBitacora)
           .HasDefaultValueSql("newId()");
        }
    }
}
