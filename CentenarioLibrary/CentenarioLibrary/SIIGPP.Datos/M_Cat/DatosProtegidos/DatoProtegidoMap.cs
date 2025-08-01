using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.DatosProtegidos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.DatosProtegidos
{
    public class DatoProtegidoMap : IEntityTypeConfiguration<DatoProtegido>
    {
        public void Configure(EntityTypeBuilder<DatoProtegido> builder)
        {
            builder.ToTable("CAT_DATO_PROTEGIDO")
                    .HasKey(a => a.IdDatosProtegidos);

            builder.Property(a => a.IdDatosProtegidos)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RAPId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDatosProtegidos)
           .HasDefaultValueSql("newId()");
        }
    }
}
