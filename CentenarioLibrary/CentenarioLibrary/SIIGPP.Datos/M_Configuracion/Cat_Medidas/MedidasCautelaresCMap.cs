using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Medidas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Medidas
{
    public class MedidasCautelaresCMap : IEntityTypeConfiguration<MedidasCautelaresC>
    {
        public void Configure(EntityTypeBuilder<MedidasCautelaresC> builder)
        {
            builder.ToTable("C_MEDIDAS_CAUTELARES")
                    .HasKey(a => a.IdMedidasCautelaresC);
            builder
           .Property(a => a.IdMedidasCautelaresC)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdMedidasCautelaresC)
           .HasDefaultValueSql("newId()");

        }
    }
}
