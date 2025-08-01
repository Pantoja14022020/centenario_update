using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_SpPi_Ligaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_SpPi_Ligaciones
{
    public class SPPiligacionesMap : IEntityTypeConfiguration<SPPiligaciones>
    {
        public void Configure(EntityTypeBuilder<SPPiligaciones> builder)
        {
            builder.ToTable("C_SPPI_LIGACIONES")
             .HasKey(a => a.IdSPPiligaciones);

            builder
           .Property(a => a.IdSPPiligaciones)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.PanelControlId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();

            builder
           .Property(a => a.DSPId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();

            builder
           .Property(a => a.IdSPPiligaciones)
           .HasDefaultValueSql("newId()");
        }
    }
}
