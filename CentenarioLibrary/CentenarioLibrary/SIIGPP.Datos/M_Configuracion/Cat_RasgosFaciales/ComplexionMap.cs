using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class ComplexionMap : IEntityTypeConfiguration<Complexion>
    {
        public void Configure(EntityTypeBuilder<Complexion> builder)
        {
            builder.ToTable("C_COMPLEXION")
                .HasKey(a => a.IdComplexion);
            builder
           .Property(a => a.IdComplexion)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdComplexion)
           .HasDefaultValueSql("newId()");
        }

    }
}
