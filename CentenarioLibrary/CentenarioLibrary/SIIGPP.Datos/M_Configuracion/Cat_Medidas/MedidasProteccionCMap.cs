using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Medidas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Medidas
{
    public class MedidasProteccionCMap : IEntityTypeConfiguration<MedidasProteccionC>
    {
        public void Configure(EntityTypeBuilder<MedidasProteccionC> builder)
        {
            builder.ToTable("C_MEDIDAS_PROTECCION")
                    .HasKey(a => a.IdMedidasProteccionC);
            builder
           .Property(a => a.IdMedidasProteccionC)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdMedidasProteccionC)
           .HasDefaultValueSql("newId()");

        }
    }
}
