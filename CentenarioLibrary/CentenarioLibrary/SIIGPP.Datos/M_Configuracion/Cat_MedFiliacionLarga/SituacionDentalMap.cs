using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_MedFiliacionLarga
{
    public class SituacionDentalMap : IEntityTypeConfiguration<SituacionDental>
    {
        public void Configure(EntityTypeBuilder<SituacionDental> builder)
        {
            builder.ToTable("C_SITUACIONDENTAL")
                   .HasKey(a => a.IdSituacionDental);

            builder
           .Property(a => a.IdSituacionDental)
           .HasDefaultValueSql("newId()");
        }
    }
}
