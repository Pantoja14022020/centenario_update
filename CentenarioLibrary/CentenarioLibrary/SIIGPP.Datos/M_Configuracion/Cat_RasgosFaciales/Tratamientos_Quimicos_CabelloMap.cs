using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tratamientos_Quimicos_CabelloMap : IEntityTypeConfiguration<Tratamientos_Quimicos_Cabello>
    {
        public void Configure(EntityTypeBuilder<Tratamientos_Quimicos_Cabello> builder)
        {
            builder.ToTable("C_TRATAMIENTO_QUIMICO_CABELLO")
                .HasKey(a => a.IdTratamientos_Quimicos_Cabello);

            builder
           .Property(a => a.IdTratamientos_Quimicos_Cabello)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdTratamientos_Quimicos_Cabello)
           .HasDefaultValueSql("newId()");
        }

    }
}
