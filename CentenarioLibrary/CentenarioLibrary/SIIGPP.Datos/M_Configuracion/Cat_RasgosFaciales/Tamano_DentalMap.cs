using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tamano_DentalMap : IEntityTypeConfiguration<Tamano_Dental>
    {
        public void Configure(EntityTypeBuilder<Tamano_Dental> builder)
        {
            builder.ToTable("C_TAMANO_DENTAL")
                .HasKey(a => a.IdTamano_Dental);

            builder
           .Property(a => a.IdTamano_Dental)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdTamano_Dental)
           .HasDefaultValueSql("newId()");
        }

    }
}
