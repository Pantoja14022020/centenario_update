using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.DDerivacion
{
    public class RDDerivacionMap : IEntityTypeConfiguration<RDDerivacion>
    {
        public void Configure(EntityTypeBuilder<RDDerivacion> builder)
        {
            builder.ToTable("CAT_RDDERIVACION")
                 .HasKey(a => a.idRDDerivacion);

            builder.Property(a => a.idRDDerivacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();


            builder.Property(a => a.FechaSys)
                 .HasColumnType("DateTime");

            builder.Property(a => a.rHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.idDDerivacion)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.idRDDerivacion)
           .HasDefaultValueSql("newId()");


        }
    }
}
