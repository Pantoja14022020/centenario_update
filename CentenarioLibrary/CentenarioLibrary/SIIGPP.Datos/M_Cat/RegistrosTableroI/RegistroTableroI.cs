using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Historialcarpetas;
using SIIGPP.Entidades.M_Cat.RegistrosTableroI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.RegistrosTableroI
{
    public class RegistroTableroIMap : IEntityTypeConfiguration<RegistroTableroI>
    {
        public void Configure(EntityTypeBuilder<RegistroTableroI> builder)
        {
            builder.ToTable("CAT_REGISTROTABLEROI")
                    .HasKey(a => a.IdRegistroTableroI);

            builder.Property(a => a.IdRegistroTableroI)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRegistroTableroI)
           .HasDefaultValueSql("newId()");
        }
    }
}
