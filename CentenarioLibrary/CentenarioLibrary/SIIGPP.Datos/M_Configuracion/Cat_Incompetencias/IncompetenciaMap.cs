using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Incompetencias;


namespace SIIGPP.Datos.M_Configuracion.Cat_Incompetencias
{
    public class IncompetenciaMap : IEntityTypeConfiguration<Incompetencia>
    {
        public void Configure(EntityTypeBuilder<Incompetencia> builder)
        {
            builder.ToTable("C_INCOMPETENCIA")
                .HasKey(a => a.IdIncompetencia);
            builder
            .Property(a => a.IdIncompetencia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdIncompetencia)
           .HasDefaultValueSql("newId()");
        }

    }
}
