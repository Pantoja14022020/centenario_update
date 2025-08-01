using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Generales
{
    public class EstadoCivilMap : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> builder)
        {
            builder.ToTable("C_ESTADO_CIVIL")
               .HasKey(a => a.IdECivil);

            builder
        .Property(a => a.IdECivil)
        .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
        .IsRequired();
            builder
           .Property(a => a.IdECivil)
           .HasDefaultValueSql("newId()");

        }
    }
}
