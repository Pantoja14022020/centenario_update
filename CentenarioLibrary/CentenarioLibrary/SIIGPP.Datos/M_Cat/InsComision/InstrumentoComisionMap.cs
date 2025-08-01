using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.InstrumentoComision;

namespace SIIGPP.Datos.M_Cat.InsComision
{
    public class InstrumentoComisionMap : IEntityTypeConfiguration<InstrumentoComision>
    {
        public void Configure(EntityTypeBuilder<InstrumentoComision> builder)
        {
            builder.ToTable("C_INSTRUMENTOSCOMISION")
                   .HasKey(a => a.IdInstrumentoComision);

            builder.Property(a => a.IdInstrumentoComision)
                   .HasColumnType("UNIQUEIDENTIFIER")
                   .IsRequired();

        }
    }
}
