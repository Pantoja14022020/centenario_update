using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Implantacion_CejaMap : IEntityTypeConfiguration<Implantacion_Ceja>
    {
        public void Configure(EntityTypeBuilder<Implantacion_Ceja> builder)
        {
            builder.ToTable("C_IMPLANTACION_CEJA")
                .HasKey(a => a.IdImplantacion_Ceja);

            builder
           .Property(a => a.IdImplantacion_Ceja)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdImplantacion_Ceja)
           .HasDefaultValueSql("newId()");
        }

    }
}
