using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RFacilitadorNotificador;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RFacilitadorNotificador
{
    public class FacilitadorNotificadorMap : IEntityTypeConfiguration<FacilitadorNotificador>
    {
        public void Configure(EntityTypeBuilder<FacilitadorNotificador> builder)
        {
            builder.ToTable("JR_FACILITADORNOTIFICADOR")
                .HasKey(a => a.IdFacilitadorNotificador);

            builder.Property(a => a.IdFacilitadorNotificador)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdFacilitadorNotificador)
           .HasDefaultValueSql("newId()");
        }
    }
}
