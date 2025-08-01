using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Largo_de_CabelloMap : IEntityTypeConfiguration<Largo_de_Cabello>
    {
        public void Configure(EntityTypeBuilder<Largo_de_Cabello> builder)
        {
            builder.ToTable("C_LARGO_DE_CABELLO")
                .HasKey(a => a.IdLargoCabello);
            builder
           .Property(a => a.IdLargoCabello)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdLargoCabello)
           .HasDefaultValueSql("newId()");
        }

    }
}
