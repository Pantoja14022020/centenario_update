using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tratamiento_DentalMap : IEntityTypeConfiguration<Tratamiento_Dental>
    {
        public void Configure(EntityTypeBuilder<Tratamiento_Dental> builder)
        {
            builder.ToTable("C_TRATAMIENTO_DENTAL")
                .HasKey(a => a.IdTratamiento_Dental);

            builder
           .Property(a => a.IdTratamiento_Dental)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdTratamiento_Dental)
           .HasDefaultValueSql("newId()");
        }

    }
}
