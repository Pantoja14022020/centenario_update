using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Delito
{
    public class DelitoEspecificoMap : IEntityTypeConfiguration<DelitoEspecifico>
    {
        public void Configure(EntityTypeBuilder<DelitoEspecifico> builder)
        {
            builder.ToTable("C_DELITO_ESPECIFICO")
                   .HasKey(a => a.IdDelitoEspecifico);


            builder.Property(a => a.IdDelitoEspecifico)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder

            .Property(a => a.DelitoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdDelitoEspecifico)
           .HasDefaultValueSql("newId()");
        }
    }
}
