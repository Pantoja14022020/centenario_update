using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class ReligionMap : IEntityTypeConfiguration<Religion>
    {
        public void Configure(EntityTypeBuilder<Religion> builder)
        {
            builder.ToTable("C_RELIGION")
                 .HasKey(a => a.IdReligion);
            builder
            .Property(a => a.IdReligion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdReligion)
           .HasDefaultValueSql("newId()");
        }
    }
}
