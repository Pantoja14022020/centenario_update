using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_ServiciosPericiales
{
    public class ServicioPericialMap : IEntityTypeConfiguration<ServicioPericial>
    {
        public void Configure(EntityTypeBuilder<ServicioPericial> builder)
        {
            builder.ToTable("SP_SERVICIOSPERICIALES")
            .HasKey(a => a.IdServicioPericial);

            builder
           .Property(a => a.IdServicioPericial)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdServicioPericial)
           .HasDefaultValueSql("newId()");
        }
    }
}
