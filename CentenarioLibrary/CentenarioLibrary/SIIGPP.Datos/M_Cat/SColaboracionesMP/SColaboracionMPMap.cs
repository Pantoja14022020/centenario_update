using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RemisionesUI;
using SIIGPP.Entidades.M_Cat.SColaboracionesMP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.SColaboracionesMP
{
    public class SColaboracionMPMap : IEntityTypeConfiguration<SColaboracionMP>
    {
        public void Configure(EntityTypeBuilder<SColaboracionMP> builder)
        {
            builder.ToTable("CAT_SCOLABORACIONMP")
                    .HasKey(a => a.IdSColaboracionMP);

            builder.Property(a => a.IdSColaboracionMP)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();
            builder.Property(a => a.AgenciaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdSColaboracionMP)
           .HasDefaultValueSql("newId()");
        }
    }
}
