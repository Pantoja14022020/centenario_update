using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_ActosInvestigacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_ActosInvestigacion
{
    public class ActoInvestigacionMap : IEntityTypeConfiguration<ActoInvestigacion>
    {
        public void Configure(EntityTypeBuilder<ActoInvestigacion> builder)
        {
            builder.ToTable("C_ACTOSINVESTIGACION")
                .HasKey(a => a.IdActonvestigacion);
            builder
            .Property(a => a.IdActonvestigacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdActonvestigacion)
           .HasDefaultValueSql("newId()");
        }

    }
}
