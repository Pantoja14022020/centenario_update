using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RDHecho;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.RDHecho
{
    public class RDHMap : IEntityTypeConfiguration<RDH>
    {
        public void Configure(EntityTypeBuilder<RDH> builder)
        {
            builder.ToTable("CAT_RDH")
                    .HasKey(a => a.IdRDH);

            builder.Property(a => a.IdRDH)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DelitoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            //builder.Property(a => a.PersonaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();

            builder
           .Property(a => a.IdRDH)
           .HasDefaultValueSql("newId()");
        }
    }
}
